using System;

namespace Fuji.RISLite.Entities
{
    public class clsAdicionales
    {
        public int intAdicionalesID { get; set; }
        public int intSitioID { get; set; }
        public int intTipoBotonID { get; set; }
        public string vchTipoBoton { get; set; }
        public int intTipoAdicionalID { get; set; }
        public string vchTipoAdicional { get; set; }
        public string vchNombreAdicional { get; set; }
        public string vchURLImagen { get; set; }
        public bool bitObservaciones { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public string vchObservaciones { get; set; }
        public string vchValor { get; set; }
        public string vchGenero { get; set; }
        public int intAdiEspecificoID { get; set; }
        public int intHombre { get; set; }
        public int intMujer { get; set; }
        public string vchEdad { get; set; }
        public int intMayor { get; set; }
        public int intMenor { get; set; }

        public clsAdicionales()
        {
            intAdicionalesID = int.MinValue;
            intSitioID = int.MinValue;
            intTipoAdicionalID = int.MinValue;
            intTipoBotonID = int.MinValue;
            vchTipoBoton = string.Empty;
            vchTipoAdicional = string.Empty;
            vchNombreAdicional = string.Empty;
            vchURLImagen = string.Empty;
            bitObservaciones = false;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            vchObservaciones = string.Empty;
            vchValor = string.Empty;
            vchGenero = string.Empty;
            intAdiEspecificoID = int.MinValue;
            intHombre = int.MinValue;
            intMujer = int.MinValue;
            vchEdad = string.Empty;
            intMayor = int.MinValue;
            intMenor = int.MinValue;
        }
    }
}
