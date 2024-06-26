﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiAutores.DTOs;
using WebApiAutores.Servicios;

namespace WebApiAutores.Utilidades
{
    public class HATEOASAutorFilterAttribute: HATEOASFiltroAttribute
    {
        private readonly GenerarEnlacesService generarEnlacesService;

        public HATEOASAutorFilterAttribute(GenerarEnlacesService generarEnlacesService)
        {
            this.generarEnlacesService = generarEnlacesService;
        }
        public override async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var debeIncluir = DebeIncluirHATEOAS(context);
            if (!debeIncluir)
            {
                await next();
                return;
            }

            var resultado = context.Result as ObjectResult;
            var autorDTO = resultado.Value as AutorDTO;
            if (autorDTO == null)
            {
                var autoresDTO = resultado.Value as List<AutorDTO> ??
                    throw new ArgumentException("Se esperaba una instancia de AutorDTO o List<AutorDTO>");

                autoresDTO.ForEach(async autor => await generarEnlacesService.GenerarEnlaces(autor));
                resultado.Value = autoresDTO;
            }
            else
            {
                await generarEnlacesService.GenerarEnlaces(autorDTO);
            }

            await next();

        }
    }
}
