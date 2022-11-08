using EnglishBattle.DAL.Entities;
using System.Reflection;
using System.Text.Json;

namespace EnglishBattle.DAL
{
    public class EnglishBattleContextSeed
    {
        public static async Task SeedAsync(EnglishBattleContext context, CancellationToken cancellationToken = default)
        {
            string ressource = "EnglishBattle.DAL.Ressources.Irregular_verbs.json";

            IrregularVerb[] verbs = await DeserializeRessourceAsync(ressource, cancellationToken);

            using (context)
            {
                var anonymousPlayer = new Player("Anonymous", "");

                context.Players.Add(anonymousPlayer);
                context.IrregularVerbs.AddRange(verbs);

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        private static async Task<IrregularVerb[]> DeserializeRessourceAsync(string ressource, CancellationToken cancellationToken)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream? ressourceStream = assembly.GetManifestResourceStream(ressource);

            if (ressourceStream is null)
            {
                throw new Exception($"Could not open ressource: {ressource}");
            }

            JsonSerializerOptions jsonOptions = new();

            IrregularVerb[]? verbs = await JsonSerializer.DeserializeAsync<IrregularVerb[]>(ressourceStream, jsonOptions, cancellationToken);

            if (verbs is null)
            {
                throw new Exception("Could not deserialize verbs");
            }

            return verbs;
        }
    }
}
