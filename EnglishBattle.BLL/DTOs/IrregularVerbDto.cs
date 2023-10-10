using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.BLL.DTOs
{
    public record IrregularVerbDto(int Id, string BaseForm, string PastParticiple, string PastSimple, string Translation);
}
