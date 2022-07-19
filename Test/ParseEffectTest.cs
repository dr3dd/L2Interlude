using System.Text.RegularExpressions;
using Xunit;

namespace Test
{
    public class ParseEffectTest
    {

        [Fact]
        public void EffectTest()
        {
            string effectA = "{{p_speed;{all};-30;per};{p_defence_attribute;attr_earth;-100}}";
            string effectB = "{{p_speed;{all};33;diff}}";

            effectA.Replace("{all}", "all");
            var pattern = @"\{.*?\}";
            var matches = Regex.Matches(effectA, pattern);
            
            string test = "{{equip_weapon;{dual;sword;blunt}};{energy_have;2}}";
            var d = Regex.Matches(test, @"\{[^}]+\}|\S+");


        }
    }
}