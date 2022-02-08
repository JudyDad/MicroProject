﻿using System.Data;
using MyWorkApplication.Classes.Selection_Wrappers;

namespace MyWorkApplication.Classes
{
    public class DataTableWrapper : ListSelectionWrapper<DataRow>
    {
        public DataTableWrapper(DataTable dataTable, string usePropertyAsDisplayName) : base(dataTable.Rows, false,
            usePropertyAsDisplayName)
        {
        }
    }
}