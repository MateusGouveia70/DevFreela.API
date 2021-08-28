using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels
{
    public class SkillViewModel
    {

        public int Id { get; private set; }
        public string Description { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is SkillViewModel model &&
                   Id == model.Id &&
                   Description == model.Description;
        }
    }
}
