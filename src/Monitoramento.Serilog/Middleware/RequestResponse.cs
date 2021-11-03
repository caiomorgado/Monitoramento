using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Serilog.Context;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Monitoramento.Middleware
{
    public class RequestResponse
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private string _request;
        public RequestResponse(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        
        public async Task Invoke(HttpContext context)
        {
            var usuario = GetUsuario(context);

            var request = await LogRequest(context);

            LogContext.PushProperty("Usuario", usuario);

            using (LogContext.PushProperty("RequestHttp", request))
            {
                await _next.Invoke(context);
            }
        }

        private string GetUsuario(HttpContext context) 
        {
            return context
                 ?.User
                 ?.Identities
                 ?.FirstOrDefault()
                 ?.Claims
                 ?.FirstOrDefault()
                 ?.Value;
        }

        private async Task<string> LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            context.Request.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Request.Body).ReadToEndAsync();

            _request = $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} {Environment.NewLine} " +
                                   $"Host: {context.Request.Host} {Environment.NewLine} " +
                                   $"Verb:{context.Request.Method} {Environment.NewLine} " +
                                   $"Path: {context.Request.Path} {Environment.NewLine} " +
                                   $"QueryString: {context.Request.QueryString} {Environment.NewLine} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}";

            context.Request.Body.Position = 0;

            return _request;
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            //await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var response = $"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} {Environment.NewLine} " +
                                   $"Host: {context.Request.Host} {Environment.NewLine} " +
                                   $"Verb:{context.Request.Method} {Environment.NewLine} " +
                                   $"Path: {context.Request.Path} {Environment.NewLine} " +
                                   $"QueryString: {context.Request.QueryString} {Environment.NewLine} " +
                                   $"Response Body: {text}";

            context.Response.Body = responseBody;
            context.Request.Body.Position = 0;

            using (LogContext.PushProperty("RequesicaoHttp", _request))
            using (LogContext.PushProperty("RespostaHttp", response))
            {
                await _next.Invoke(context);
            }

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
