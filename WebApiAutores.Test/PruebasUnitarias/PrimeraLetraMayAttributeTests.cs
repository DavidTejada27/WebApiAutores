using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Test.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayAttributeTests
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            //Preparacion
            var primeraLetraMayuscula = new PrimeraLetraMayAttribute();
            var valor = "david";
            var valContext = new ValidationContext(new { Nombre = valor });
            //Ejecucion

            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);
            //Verificacion

            Assert.AreEqual("La primera letra debe ser mayuscula", resultado.ErrorMessage);
        }
    }
}