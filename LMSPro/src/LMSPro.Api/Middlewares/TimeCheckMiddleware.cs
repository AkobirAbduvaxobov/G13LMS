using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LMSPro.Api.Middlewares
{
    public class TimeCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimeCheckMiddleware> _logger;

        public TimeCheckMiddleware(RequestDelegate next, ILogger<TimeCheckMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // 1. Hozirgi vaqtning minut qismini olamiz
            int currentMinute = DateTime.Now.Minute;

            // 2. Minut toq yoki juftligini tekshiramiz
            if (currentMinute % 2 != 0)
            {
                // Toq minutda kelsa - yo'lini to'samiz (Short-circuiting)
                _logger.LogWarning("So'rov rad etildi! Vaqt: {CurrentTime}. Minut toq ({CurrentMinute})",
                    DateTime.Now.ToString("HH:mm:ss"), currentMinute);

                // Klientga Bad Request (400) yoki o'zingiz xohlagan status kodni qaytaramiz
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Omadsiz urinish! So'rov faqat juft minutlarda qabul qilinadi.",
                    CurrentMinute = currentMinute
                });

                // Diqqat! Bu yerda await _next(context) chaqirilmaydi! 
                // Pipeline shu yerda to'xtaydi va Controller'ga o'tmaydi.
                return;
            }

            // 3. Agar juft minut bo'lsa - log yozib, yo'lida davom etishiga ruxsat beramiz
            _logger.LogInformation("So'rov muvaffaqiyatli o'tdi. Vaqt: {CurrentTime}. Minut juft ({CurrentMinute})",
                DateTime.Now.ToString("HH:mm:ss"), currentMinute);

            // Keyingi middleware yoki Controller'ga uzatish
            await _next(context);
        }
    }
}
