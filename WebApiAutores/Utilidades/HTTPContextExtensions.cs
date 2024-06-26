﻿using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace WebApiAutores.Utilidades
{
    public static class HTTPContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));

            }

            double cantidad = await queryable.CountAsync();
            httpContext.Response.Headers.Add("cantidadTotalRegistros", cantidad.ToString());
        }

    }
}
