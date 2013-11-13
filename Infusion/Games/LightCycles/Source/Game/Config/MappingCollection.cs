namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Collection of mappings
    /// </summary>
    public class MappingCollection : List<Mapping>
    {
        /// <summary>
        /// Find mapping by id
        /// </summary>
        /// <param name="id">id to look for</param>
        /// <returns>found mapping</returns>
        public Mapping FindById(char id)
        {
            foreach (Mapping mapping in this)
            {
                if (mapping.Id.Equals(id))
                {
                    return mapping;
                }
            }

            return null;
        }
    }
}
