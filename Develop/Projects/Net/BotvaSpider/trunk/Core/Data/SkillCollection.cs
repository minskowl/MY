using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotvaSpider.Core;
using WatiN.Core.Interfaces;

namespace BotvaSpider.Data
{
    public class SkillCollection : IEnumerable<Skill>
    {
        Dictionary<SkillType, Skill> storage = new Dictionary<SkillType, Skill>();

        /// <summary>
        /// Gets or sets the <see cref="Skill"/> with the specified type.
        /// </summary>
        /// <value></value>
        public Skill this[SkillType type]
        {
            get
            {
                return storage.ContainsKey(type) ? storage[type] : null;
            }
            set
            {
                if (storage.ContainsKey(type))
                    storage[type] = value;
                else
                    storage.Add(type, value);
            }
        }
        /// <summary>
        /// Sets the skill.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="points">The points.</param>
        public void SetSkill(SkillType type, double points)
        {
            SetSkill(type, points, 0);
        }


        /// <summary>
        /// Sets the skill.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="points">The points.</param>
        /// <param name="additionalPoints">The additional points.</param>
        public void SetSkill(SkillType type, double points, double additionalPoints)
        {
            Skill skill;
            if (storage.ContainsKey(type))
            {
                skill = storage[type];
                skill.Points = points;
                skill.AdditionalPoints = additionalPoints;
            }
            else
            {
                skill = new Skill
                            {
                                SkilType = type,
                                Points = points,
                                AdditionalPoints = additionalPoints
                            };
                storage.Add(type, skill);
            }


        }

        /// <summary>
        /// Fills the skils.
        /// </summary>
        /// <param name="skillCollection">The skill collection.</param>
        public void FillSkils(SkillCollection skillCollection)
        {
            foreach (var skill in skillCollection)
            {
                if (skill.AdditionalPoints > 0)
                    SetSkill(skill.SkilType, skill.Points, skill.AdditionalPoints);
                else
                    SetSkill(skill.SkilType, skill.Points);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Skill> GetEnumerator()
        {
            return storage.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }




     
    }
}