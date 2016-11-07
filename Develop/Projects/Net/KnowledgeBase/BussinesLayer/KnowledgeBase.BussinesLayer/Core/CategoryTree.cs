using System;
using System.Collections.Generic;

namespace KnowledgeBase.BussinesLayer.Core
{
    public class CategoryTree
    {
        private readonly Dictionary<int, int[]> _childrensMap = new Dictionary<int, int[]>();
        private readonly KbContext _context;

        private int _maxId;


        /// <summary>
        /// Gets or sets the max id.
        /// </summary>
        /// <value>The max id.</value>
        public int MaxId
        {
            get { return _maxId; }
            set { _maxId = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryTree"/> class.
        /// </summary>
        public CategoryTree(KbContext context)
        {
            _context = context;
            LoadData();
        }

        /// <summary>
        /// Gets the child.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public int[] GetChild(int id)
        {
            return _childrensMap.ContainsKey(id) ? _childrensMap[id] : new int[0];
        }



        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void Reload()
        {
            LoadData();
        }
        private void LoadData()
        {
            var data = _context.ManagerCategory.GetTree();

            var currentParentId = 0;
            var childrens = new List<int>();
            _childrensMap.Clear();

            foreach (var pair in data)
            {

                if (pair.ParentId > _maxId) _maxId = pair.ParentId;
                if (pair.Id > _maxId) _maxId = pair.Id;

                if (currentParentId != pair.ParentId) //Need save temp
                {
                    _childrensMap.Add(currentParentId, childrens.ToArray());
                    currentParentId = pair.ParentId;
                    childrens = new List<int>();
                }

                childrens.Add(pair.Id);

            }
            if (childrens.Count > 0)
                _childrensMap.Add(currentParentId, childrens.ToArray());


        }
    }
}
