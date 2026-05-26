using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mbg
{
    namespace Module
    {
        namespace jqGridModule
        {
            public class jqGrid {
                private String _caption;
                private String _url;
                private String _editUrl;
                private String _dataType;
                private String[] _columnName;
                private List<ColumnModel> _columnModels;
                private bool _rowNumbers;
                private int _rowNum;
                private int[] _rowList;
                private String _sortName;
                private String _sortOrder;
                private bool _viewRecords;
                private String _pager;
                private bool _autoWidth;
                private int _width;
                private int _height;
                private bool _altRows;
                private String _loadError, _gridComplete, _ondblClickRow;

            }

            public class ColumnModel {
                private bool _key;
                private String _index;
                private String _name;
                private String _label;
                private String _align;
                private bool _editable;
                private String _edittype;
                private bool _sortable;
                private List<EditOption> editOptions;
                private bool _hidden;
                private bool _search;
                
            }

            public class EitRules {
                private bool _editHidden, _required, _number, _integer
                    , _email, _url, _date, _time, _custom;
                private int _minValue, _maxValue;
                private String _custom_func;
            }

            public class FormOption {
                private String _elmPrefix, _elmSuffix, _label;
                private int _rowpos, _colpos;
            }

            public class EditOption {
                private object defaultValue;
                private String value;
                private String dataUrl;
                private String dataInit;
                private bool _nullIfEmpty;
            }
        }
    }
}