namespace POS.Utilities.Static
{
    public class ExcelColumnNames
    {
        public static List<TableColumns> GetColumns(IEnumerable<(string ColumnName, string PropertyName)> columnsProperties)
        {
            var columns = new List<TableColumns>();

            foreach (var (ColumnName, PropertyName) in columnsProperties)
            {
                var column = new TableColumns()
                {
                    Label = ColumnName,
                    PropertyName = PropertyName
                };

                columns.Add(column);
            }

            return columns;
        }

        #region ColumnsCategories

        public static List<(string columnName, string PropertyName)> GetColumnsCategories()
        {
            var columnsProperties = new List<(string columnName, string PropertyName)>
            {
                ("NOMBRE", "Name"),
                ("DESCRIPCIÓN", "Description"),
                ("FECHA DE CREACIÓN", "AuditCreate"),
                ("ESTADO", "StateCategory"),
            };

            return columnsProperties;
        }

        #endregion

        #region ColumnsProviders

        public static List<(string columnName, string PropertyName)> GetColumnsProviders()
        {
            var columnsProperties = new List<(string columnName, string PropertyName)>
            {
                ("NOMBRE", "Name"),
                ("EMAIL", "Email"),
                ("TIPO DE DOCUMENTO", "DocumentType"),
                ("Nº DOCUMENTO", "DocumentNumber"),
                ("DIRECCIÓN", "Address"),
                ("TELEFONO", "Phone"),
                ("FECHA DE CREACION", "AuditCreateDate"),
                ("ESTADO", "StateCategory"),
            };

            return columnsProperties;
        }

        #endregion
    }
}
