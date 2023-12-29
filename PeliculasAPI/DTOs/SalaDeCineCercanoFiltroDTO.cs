using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class SalaDeCineCercanoFiltroDTO
    {
        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }
        public int distanciaEnKms { get; set; } = 10;
        public int distanciaMaximaKms { get; set; } = 50;
        public  int DistanciaEnKms
        {
            get 
            {
                return distanciaEnKms; 
            }
            set
            {
                distanciaEnKms = (value > distanciaMaximaKms) ? distanciaMaximaKms : value;
            }
        }
    }
}
