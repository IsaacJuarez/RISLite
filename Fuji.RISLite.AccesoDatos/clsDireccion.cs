namespace Fuji.RISLite.Entities
{
    public class clsDireccion
    {
        public int intDireccionID { get; set; }
        public int intCodigoPostalID { get; set; }
        public string vchCodigoPostal { get; set; }
        public string vchColonia { get; set; }
        public int intMunicipioID { get; set; }
        public string vchMunicipio { get; set; }
        public int intEstadoID { get; set; }
        public string vchEstado { get; set; }
        public string vchCalle { get; set; }
        public string vchNumero { get; set; }

        public clsDireccion()
        {
            intDireccionID = int.MinValue;
            intCodigoPostalID = int.MinValue;
            vchCodigoPostal = string.Empty;
            vchColonia = string.Empty;
            intMunicipioID = int.MinValue;
            vchMunicipio = string.Empty;
            intEstadoID = int.MinValue;
            vchEstado = string.Empty;
            vchCalle = string.Empty;
            vchNumero = string.Empty;
        }
    }
}
