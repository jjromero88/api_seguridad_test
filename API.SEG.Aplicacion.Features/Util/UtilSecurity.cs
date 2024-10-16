using API.SEG.Domain.Entities;

namespace API.SEG.Aplicacion.Features.Util
{
    public static class UtilSecurity
    {
        public static Accesos? BuscarAccesoPorUrl(List<Accesos> accesos, string urlOpcion)
        {
            foreach (var acceso in accesos)
            {
                // Si la url_opcion coincide con el acceso actual, lo devuelve.
                if (acceso.url_opcion == urlOpcion)
                {
                    return acceso;
                }

                // Si hay una sublista (lista_accesos) no nula, busca recursivamente en esa sublista.
                if (acceso.lista_accesos != null && acceso.lista_accesos.Any())
                {
                    var accesoEncontrado = BuscarAccesoPorUrl(acceso.lista_accesos, urlOpcion);

                    // Si se encuentra algo en la sublista, devuelve el acceso encontrado.
                    if (accesoEncontrado != null && !string.IsNullOrEmpty(accesoEncontrado.url_opcion))
                    {
                        return accesoEncontrado;
                    }
                }
            }

            // Si no se encuentra el acceso en ningún nivel, devuelve null.
            return null;
        }

    }
}
