using _Net.Models;
using System.Collections.Generic;

namespace _Net.TestData
{
    public static class SampleTodoItems
    {
        public static async Task<List<TodoItem>> GetSampleTasksAsync()
        {
            var allImageInfos = await ImageHelper.GetAllImageInfosAsync();

            var sampleTasks = new List<TodoItem>
            {
                  new TodoItem
                {
                    Title = "Riego adecuado",
                    Description = "Asegurarse de que las plantas reciban la cantidad correcta de agua según sus necesidades específicas, evitando el exceso o la falta de humedad en el sustrato.",
                    Img = allImageInfos[0],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Podado y eliminación de hojas marchitas",
                    Description = "Retirar regularmente las hojas muertas o enfermas para fomentar el crecimiento saludable y prevenir la propagación de enfermedades.",
                    Img = allImageInfos[1],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Fertilización y enriquecimiento del sustrato",
                    Description = "Proporcionar nutrientes esenciales a las plantas mediante la aplicación de fertilizantes, manteniendo el sustrato rico en nutrientes.",
                    Img = allImageInfos[2],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Control de plagas y enfermedades",
                    Description = "Inspeccionar las plantas en busca de signos de plagas o enfermedades, y tomar medidas para prevenir o tratar los problemas.",
                    Img = allImageInfos[3],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Repotting o trasplante",
                    Description = "Trasladar las plantas a macetas más grandes cuando sus raíces hayan superado el espacio actual, promoviendo un crecimiento continuo.",
                    Img = allImageInfos[4],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Exposición adecuada a la luz solar y sombra",
                    Description = "Colocar las plantas en lugares donde reciban la cantidad adecuada de luz solar directa o indirecta según sus preferencias.",
                    Img = allImageInfos[5],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Apoyo y entrenamiento de plantas trepadoras",
                    Description = "Proporcionar estructuras de soporte y guiar el crecimiento de las plantas trepadoras para un desarrollo vertical ordenado.",
                    Img = allImageInfos[6],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Rotación de plantas",
                    Description = "Girar las plantas periódicamente para garantizar que todas las partes reciban una exposición uniforme a la luz, evitando el crecimiento desigual.",
                    Img = allImageInfos[7],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Control de humedad y ventilación en interiores",
                    Description = "Mantener niveles de humedad adecuados y asegurar una buena circulación de aire en espacios interiores para evitar problemas como el moho.",
                    Img = allImageInfos[8],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TodoItem
                {
                    Title = "Investigación y selección de plantas apropiadas",
                    Description = "Elegir plantas que se adapten al entorno y a las condiciones de cuidado disponibles, maximizando sus posibilidades de crecimiento exitoso.",
                    Img = allImageInfos[9],
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
            };

            return sampleTasks;
        }
    }
}
