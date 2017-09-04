namespace Fuji.RISLite.Entities
{
    public class clsPrestacion
    {
        public int intRELModPres { get; set; }
        public int intModalidadID { get; set; }
        public string vchModalidad { get; set; }
        public int intPrestacionID { get; set; }
        public string vchPrestacion { get; set; }
        public int intDuracionMin { get; set; }
        public bool bitActivo { get; set; }

        public clsPrestacion()
        {
            intRELModPres = int.MinValue;
            intModalidadID = int.MinValue;
            vchModalidad = string.Empty;
            intPrestacionID = int.MinValue;
            vchPrestacion = string.Empty;
            intDuracionMin = int.MinValue;
            bitActivo = false;
        }
    }
}
