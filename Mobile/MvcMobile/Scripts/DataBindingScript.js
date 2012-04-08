// MvcDemoScript.js
//


Type.registerNamespace('DataBindingScript');

////////////////////////////////////////////////////////////////////////////////
// DataBindingScript.JsonResult

DataBindingScript.JsonResult = function DataBindingScript_JsonResult() {
}
DataBindingScript.JsonResult.prototype = {
    get_item: function DataBindingScript_JsonResult$get_item(key) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="Object">
        /// </param>
        /// <returns type="Object"></returns>
        return null;
    },
    set_item: function DataBindingScript_JsonResult$set_item(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="Object">
        /// </param>
        /// <returns type="Object"></returns>
        return value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// DataBindingScript.DataRepeater

DataBindingScript.DataRepeater = function DataBindingScript_DataRepeater(selector, altSelector) {
    /// <param name="selector" type="String">
    /// </param>
    /// <param name="altSelector" type="String">
    /// </param>
    /// <field name="_parent" type="Object" domElement="true">
    /// </field>
    /// <field name="_itemRep" type="Object" domElement="true">
    /// </field>
    /// <field name="_itemRep2" type="Object" domElement="true">
    /// </field>
    /// <field name="_position" type="Number" integer="true">
    /// </field>
    /// <field name="_itemCount" type="Number" integer="true">
    /// </field>
    this._position = -1;
    var jTable = jQuery(selector);
    var altJTable = jQuery(altSelector);
    if (jTable.length > 0) {
        this._parent = jTable[0].parentNode;
        this._itemRep = jTable[0];
        this._itemRep.id = 'jQ' + Math.random().toString().substring(2, 10);
        if (altSelector != null && altSelector !== '') {
            this._itemRep2 = altJTable[0];
            this._itemRep2.id = 'jQ' + Math.random().toString().substring(2, 10);
        }
        var count = this._parent.childNodes.length;
        for (var i = 0; i < count; i++) {
            if (this._parent.childNodes[i] === this._itemRep) {
                this._position = i;
                break;
            }
        }
        this._parent.removeChild(this._itemRep);
        if (this._itemRep2 != null) {
            this._parent.removeChild(this._itemRep2);
        }
    }
}
DataBindingScript.DataRepeater.prototype = {
    _parent: null,
    _itemRep: null,
    _itemRep2: null,
    _itemCount: 0,
    
    bindData: function DataBindingScript_DataRepeater$bindData(dataJson, flags, onBinding) {
        /// <param name="dataJson" type="Array" elementType="JsonResult">
        /// </param>
        /// <param name="flags" type="Object">
        /// </param>
        /// <param name="onBinding" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        if (this._position < 0) {
            return;
        }
        var dataCount = dataJson.length;
        for (var i = this._itemCount - 1; flags == null && i >= 0; i--) {
            var child = this._parent.childNodes[i + this._position];
            this._parent.removeChild(child);
        }
        var adv = 0;
        if (flags === -1) {
            adv = this._itemCount;
        }
        else if (flags > -1) {
            adv = flags;
        }
        this._itemCount = ((flags == null) ? 0 : this._itemCount) + dataCount;
        if (this._position + adv < this._parent.childNodes.length) {
            var itemRef = this._parent.childNodes[this._position + adv];
            var obj = new DataBindingScript.DataBinding();
            var isAlt = false;
            for (var i = 0; i < dataCount; i++) {
                var item = (isAlt && this._itemRep2 != null) ? this._itemRep2.cloneNode(true) : this._itemRep.cloneNode(true);
                item.id = String.format('{0}_child_{1}', item.id, i + adv);
                this._parent.insertBefore(item, itemRef);
                obj.bindData(dataJson[i], item.id);
                isAlt = !isAlt;
                if (onBinding != null) {
                    onBinding(item);
                }
            }
        }
        else if (this._position + adv === this._parent.childNodes.length) {
            var obj = new DataBindingScript.DataBinding();
            var isAlt = false;
            for (var i = 0; i < dataCount; i++) {
                var item = (isAlt && this._itemRep2 != null) ? this._itemRep2.cloneNode(true) : this._itemRep.cloneNode(true);
                item.id = String.format('{0}_child_{1}', item.id, i + adv);
                this._parent.appendChild(item);
                obj.bindData(dataJson[i], item.id);
                isAlt = !isAlt;
                if (onBinding != null) {
                    onBinding(item);
                }
            }
        }
    },
    
    bindDataHref: function DataBindingScript_DataRepeater$bindDataHref(href, onSuccess, onDeny) {
        /// <param name="href" type="String">
        /// </param>
        /// <param name="onSuccess" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        /// <param name="onDeny" type="DataBindingScript.SimpleDelegate">
        /// </param>
        jQuery.ajaxSetup({ cache: false });
        jQuery('').ajaxError(Function.createDelegate(this, function(data1, data2, data3) {
            alert('error');
        }));
        jQuery.getJSON(href, {}, Function.createDelegate(this, function(data) {
            if (data === DataBindingScript.DataBinding.authenFail && onDeny != null) {
                onDeny();
            }
            this.bindData(data, null, null);
            if (onSuccess != null) {
                onSuccess(data);
            }
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// DataBindingScript.DataGrid

DataBindingScript.DataGrid = function DataBindingScript_DataGrid(elementId) {
    /// <param name="elementId" type="String">
    /// </param>
    /// <field name="_vals" type="Array" elementType="String">
    /// </field>
    /// <field name="_cols" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_row" type="Object" domElement="true">
    /// </field>
    /// <field name="_rowAltClass" type="String">
    /// </field>
    /// <field name="_table" type="Object" domElement="true">
    /// </field>
    /// <field name="_tableTmp" type="Object" domElement="true">
    /// </field>
    var selector = ((elementId !== '') ? '#' : '') + elementId;
    var jTable = jQuery(selector);
    if (jTable.length > 0 && jTable[0].tagName.toLowerCase() === 'table') {
        var selector1 = String.format('{0} tr td[BindName]', selector);
        var fields = jQuery(selector1);
        var count = fields.length;
        if (count > 0) {
            this._vals = new Array(count);
            this._cols = new Array(count);
            this._row = fields[0].parentNode;
            this._rowAltClass = this._row.getAttribute('RowAlt');
            if (this._rowAltClass != null && this._rowAltClass !== '') {
                this._row.removeAttribute('RowAlt');
            }
            this._table = this._row.parentNode;
        }
        for (var i = 0; i < count; i++) {
            var name = fields[i].getAttribute('BindName');
            var selector2 = String.format('{0} tr td[BindName=\"{1}\"][BindValue], {0} tr td[BindName=\"{1}\"] [BindValue]', selector, name);
            var fieldValues = jQuery(selector2);
            var countValues = fieldValues.length;
            var vals = '';
            for (var j = 0; j < countValues; j++) {
                vals += ((fields[i] === fieldValues[j]) ? 'm' : this._getPath(fields[i], fieldValues[j])) + ';';
            }
            this._vals[i] = (vals.length > 0) ? vals.substr(0, vals.length - 1) : vals;
            this._cols[i] = DataBindingScript.DataBinding.detachElement(fields[i]);
        }
        if (this._row != null) {
            this._row.parentNode.removeChild(this._row);
        }
    }
}
DataBindingScript.DataGrid.prototype = {
    _vals: null,
    _cols: null,
    _row: null,
    _rowAltClass: '',
    _table: null,
    _tableTmp: null,
    
    _setTextContent: function DataBindingScript_DataGrid$_setTextContent(element, text) {
        /// <param name="element" type="Object" domElement="true">
        /// </param>
        /// <param name="text" type="String">
        /// </param>
        if (DataBindingScript.DataBinding.isFireFox()) {
            while (element.firstChild != null) {
                element.removeChild(element.firstChild);
            }
            element.appendChild(document.createTextNode(text));
        }
        else {
            element.innerText = text;
        }
    },
    
    _getPath: function DataBindingScript_DataGrid$_getPath(parent, element) {
        /// <param name="parent" type="Object" domElement="true">
        /// </param>
        /// <param name="element" type="Object" domElement="true">
        /// </param>
        /// <returns type="String"></returns>
        if (parent === element) {
            return '';
        }
        var result = '';
        var count = parent.childNodes.length;
        for (var i = 0; i < count; i++) {
            if (parent.childNodes[i] === element) {
                result += i.toString();
                break;
            }
            else {
                var tmpStr = this._getPath(parent.childNodes[i], element);
                if (tmpStr.length > 0) {
                    result += i.toString() + ',' + tmpStr;
                    break;
                }
            }
        }
        return result;
    },
    
    bindData: function DataBindingScript_DataGrid$bindData(dataJson) {
        /// <param name="dataJson" type="Array" elementType="JsonResult">
        /// </param>
        if (this._cols == null) {
            return;
        }
        if (this._cols.length === 0) {
            return;
        }
        var dataCount = dataJson.length;
        var count = this._cols.length;
        if (this._tableTmp == null) {
            this._tableTmp = this._table.cloneNode(true);
        }
        var table = this._tableTmp.cloneNode(true);
        this._table.parentNode.replaceChild(table, this._table);
        this._table = table;
        for (var i = 0; i < dataCount; i++) {
            var tr = this._row.cloneNode(true);
            if (i % 2 === 1 && this._rowAltClass != null && this._rowAltClass !== '') {
                tr.setAttribute('class', this._rowAltClass);
            }
            this._table.appendChild(tr);
            for (var j = 0; j < count; j++) {
                var field = this._cols[j].cloneNode(true);
                tr.appendChild(field);
                var name = field.getAttribute('BindName');
                field.removeAttribute('BindName');
                var vals = this._vals[j].split(';');
                var countVal = vals.length;
                for (var k = 0; k < countVal; k++) {
                    if (vals[k] === 'm') {
                        field.removeAttribute('BindValue');
                        this._setTextContent(field, dataJson[i][name]);
                    }
                    else {
                        var childIndexs = vals[k].split(',');
                        var childIndexsCount = childIndexs.length;
                        var elementCur = field;
                        for (var l = 0; l < childIndexsCount; l++) {
                            elementCur = elementCur.childNodes[childIndexs[l]];
                        }
                        var valueType = elementCur.getAttribute('BindValue');
                        elementCur.removeAttribute('BindValue');
                        if (valueType === 'inner') {
                            this._setTextContent(elementCur, dataJson[i][name]);
                        }
                        else {
                            elementCur.setAttribute(valueType, dataJson[i][name]);
                        }
                    }
                }
            }
        }
    },
    
    bindDataHref: function DataBindingScript_DataGrid$bindDataHref(href, onSuccess, onDeny) {
        /// <param name="href" type="String">
        /// </param>
        /// <param name="onSuccess" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        /// <param name="onDeny" type="DataBindingScript.SimpleDelegate">
        /// </param>
        jQuery.ajaxSetup({ cache: false });
        jQuery('').ajaxError(Function.createDelegate(this, function(data1, data2, data3) {
            alert('error');
        }));
        jQuery.getJSON(href, {}, Function.createDelegate(this, function(data) {
            if (data === DataBindingScript.DataBinding.authenFail && onDeny != null) {
                onDeny();
            }
            this.bindData(data);
            if (onSuccess != null) {
                onSuccess(data);
            }
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// DataBindingScript.DataBinding

DataBindingScript.DataBinding = function DataBindingScript_DataBinding() {
    /// <field name="authenFail" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_queueBindDataComplete" type="Number" integer="true">
    /// </field>
    /// <field name="onSuccessBindData" type="JQuerySharp.EmptyEventHandler">
    /// </field>
}
DataBindingScript.DataBinding.isFireFox = function DataBindingScript_DataBinding$isFireFox() {
    /// <returns type="Boolean"></returns>
    return window.navigator.appName === 'Netscape';
}
DataBindingScript.DataBinding.equal = function DataBindingScript_DataBinding$equal(data1, data2) {
    /// <param name="data1" type="Object">
    /// </param>
    /// <param name="data2" type="Object">
    /// </param>
    /// <returns type="Boolean"></returns>
    var result = true;
    var $dict1 = data1;
    for (var $key2 in $dict1) {
        var item = { key: $key2, value: $dict1[$key2] };
        if (item.value !== data2[item.key]) {
            result = false;
            break;
        }
    }
    return result;
}
DataBindingScript.DataBinding.clearDataForm = function DataBindingScript_DataBinding$clearDataForm(parentId) {
    /// <param name="parentId" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    var selector = (parentId !== '') ? '#{0} input[BindName], #{0} select[BindName], #{0} textarea[BindName]' : 'input[BindName], select[BindName], textarea[BindName]';
    selector = String.format(selector, parentId);
    var fields = jQuery(selector);
    var count = fields.length;
    for (var i = 0; i < count; i++) {
        var field = fields[i];
        var fieldName = field.getAttribute('BindName');
        if (field.tagName.toLowerCase() === 'input') {
            var fieldType = field.getAttribute('type');
            switch (fieldType) {
                case 'text':
                case 'hidden':
                case 'password':
                    (field).value = '';
                    break;
                case 'checkbox':
                    (field).checked = false;
                    break;
                case 'image':
                    field.setAttribute('src', '');
                    break;
                case 'radio':
                    break;
                default:
                    break;
            }
        }
        else if (field.tagName.toLowerCase() === 'select') {
            (field).value = '';
        }
        else if (field.tagName.toLowerCase() === 'textarea') {
            (field).value = '';
        }
    }
    return true;
}
DataBindingScript.DataBinding._containString = function DataBindingScript_DataBinding$_containString(source, str) {
    /// <param name="source" type="Array">
    /// </param>
    /// <param name="str" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    var count = source.length;
    for (var i = 0; i < count; i++) {
        if (source[i] === str) {
            return true;
        }
    }
    return false;
}
DataBindingScript.DataBinding.serializeDataEachElement = function DataBindingScript_DataBinding$serializeDataEachElement(selectorMask) {
    /// <param name="selectorMask" type="Object" domElement="true">
    /// </param>
    /// <returns type="Object"></returns>
    var selectorElement = 'input[BindName], select[BindName], textarea[BindName]';
    var fields = (selectorMask).querySelectorAll(selectorElement);
    var dataForm = {};
    var count = fields.length;
    for (var i = 0; i < count; i++) {
        var field = fields[i];
        var fieldName = field.getAttribute('BindName');
        if (field.tagName.toLowerCase() === 'input') {
            var fieldType = field.getAttribute('type');
            switch (fieldType) {
                case null:
                case '':
                case 'text':
                case 'hidden':
                case 'password':
                    dataForm[fieldName] = (field).value;
                    break;
                case 'checkbox':
                    dataForm[fieldName] = (field).checked;
                    break;
                case 'image':
                    dataForm[fieldName] = field.getAttribute('src');
                    break;
                case 'radio':
                    dataForm[fieldName] = DataBindingScript.DataBinding._getValueRadioSelector(field, selectorMask);
                    break;
                default:
                    break;
            }
        }
        else if (field.tagName.toLowerCase() === 'select') {
            dataForm[fieldName] = (field).value;
        }
        else if (field.tagName.toLowerCase() === 'textarea') {
            dataForm[fieldName] = (field).value;
        }
    }
    return dataForm;
}
DataBindingScript.DataBinding.serializeDataArray = function DataBindingScript_DataBinding$serializeDataArray(selector) {
    /// <param name="selector" type="String">
    /// </param>
    /// <returns type="Array"></returns>
    var rows = jQuery(selector);
    var count = rows.length;
    var result = [];
    for (var i = 0; i < count; i++) {
        result = result.concat(DataBindingScript.DataBinding.serializeDataEachElement(rows[i]));
    }
    return result;
}
DataBindingScript.DataBinding.serializeData = function DataBindingScript_DataBinding$serializeData(parentId) {
    /// <param name="parentId" type="String">
    /// </param>
    /// <returns type="Object"></returns>
    var selector = (parentId !== '') ? '#{0} input[BindName], #{0} select[BindName], #{0} textarea[BindName]' : 'input[BindName], select[BindName], textarea[BindName]';
    selector = String.format(selector, parentId);
    var fields = jQuery(selector);
    var dataForm = {};
    var count = fields.length;
    for (var i = 0; i < count; i++) {
        var field = fields[i];
        var fieldName = field.getAttribute('BindName');
        if (field.tagName.toLowerCase() === 'input') {
            var fieldType = field.getAttribute('type');
            switch (fieldType) {
                case null:
                case '':
                case 'text':
                case 'hidden':
                case 'password':
                    dataForm[fieldName] = (field).value;
                    break;
                case 'checkbox':
                    dataForm[fieldName] = (field).checked;
                    break;
                case 'image':
                    dataForm[fieldName] = field.getAttribute('src');
                    break;
                case 'radio':
                    dataForm[fieldName] = DataBindingScript.DataBinding._getValueRadio(field, parentId);
                    break;
                default:
                    break;
            }
        }
        else if (field.tagName.toLowerCase() === 'select') {
            dataForm[fieldName] = (field).value;
        }
        else if (field.tagName.toLowerCase() === 'textarea') {
            dataForm[fieldName] = (field).value;
        }
    }
    return dataForm;
}
DataBindingScript.DataBinding.serializeData2 = function DataBindingScript_DataBinding$serializeData2(parentId) {
    /// <param name="parentId" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var result = '{';
    var tmp = DataBindingScript.DataBinding.serializeData(parentId);
    var $dict1 = tmp;
    for (var $key2 in $dict1) {
        var item = { key: $key2, value: $dict1[$key2] };
        result += (String.format('\'{0}\':\'{1}\'', item.key, item.value) + ',');
    }
    result = result.substr(0, result.length - 1) + '}';
    return result;
}
DataBindingScript.DataBinding.detachElement = function DataBindingScript_DataBinding$detachElement(element) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Object" domElement="true"></returns>
    if (element == null) {
        return null;
    }
    element.parentNode.removeChild(element);
    return element;
}
DataBindingScript.DataBinding.detachElementId = function DataBindingScript_DataBinding$detachElementId(parentId) {
    /// <param name="parentId" type="String">
    /// </param>
    /// <returns type="Object" domElement="true"></returns>
    if (parentId === '') {
        return null;
    }
    var jElement = jQuery(String.format('#{0}', parentId));
    if (jElement.length === 0) {
        return null;
    }
    var result = jElement[0];
    result.parentNode.removeChild(result);
    return result;
}
DataBindingScript.DataBinding._setModelStatus = function DataBindingScript_DataBinding$_setModelStatus(dataJson, field, name) {
    /// <param name="dataJson" type="DataBindingScript.JsonResult">
    /// </param>
    /// <param name="field" type="Object" domElement="true">
    /// </param>
    /// <param name="name" type="String">
    /// </param>
    if (dataJson['ModelStatus'] != null) {
        var fieldAtt = (dataJson['ModelStatus'])[name];
        if (fieldAtt != null) {
            var $dict1 = fieldAtt;
            for (var $key2 in $dict1) {
                var att = { key: $key2, value: $dict1[$key2] };
                field.setAttribute(att.key, att.value);
            }
        }
    }
}
DataBindingScript.DataBinding._getValueRadioSelector = function DataBindingScript_DataBinding$_getValueRadioSelector(radioField, element) {
    /// <param name="radioField" type="Object" domElement="true">
    /// </param>
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="String"></returns>
    var selector = String.format('input[popupId=\"{0}\"]', radioField.getAttribute('popupId'));
    var fields = (element).querySelectorAll(selector);
    var result = '';
    var count = fields.length;
    for (var i = 0; i < count; i++) {
        var radioItem = fields[i];
        if (radioItem.checked) {
            result = radioItem.value;
            break;
        }
    }
    return result;
}
DataBindingScript.DataBinding._getValueRadio = function DataBindingScript_DataBinding$_getValueRadio(radioField, parentId) {
    /// <param name="radioField" type="Object" domElement="true">
    /// </param>
    /// <param name="parentId" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var selector = ((parentId !== '') ? '#' : '') + parentId;
    selector += String.format(' input[popupId=\"{0}\"]', radioField.getAttribute('popupId'));
    var fields = jQuery(selector);
    var result = '';
    var count = fields.length;
    for (var i = 0; i < count; i++) {
        var radioItem = fields[i];
        if (radioItem.checked) {
            result = radioItem.value;
            break;
        }
    }
    return result;
}
DataBindingScript.DataBinding.prototype = {
    
    postData: function DataBindingScript_DataBinding$postData(href, parentId, onSuccess, onDeny) {
        /// <param name="href" type="String">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        /// <param name="onSuccess" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        /// <param name="onDeny" type="DataBindingScript.SimpleDelegate">
        /// </param>
        var dataForm = DataBindingScript.DataBinding.serializeData(parentId);
        jQuery.ajaxSetup({ cache: false });
        jQuery.post(href, dataForm, Function.createDelegate(this, function(data) {
            if (data === DataBindingScript.DataBinding.authenFail && onDeny != null) {
                onDeny();
            }
            eval('data = ' + data);
            if (onSuccess != null) {
                onSuccess(data);
            }
        }));
    },
    
    postDataAjax: function DataBindingScript_DataBinding$postDataAjax(href, parentId, onSuccess, onFailed) {
        /// <param name="href" type="String">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        /// <param name="onSuccess" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        /// <param name="onFailed" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        var dataForm = DataBindingScript.DataBinding.serializeData2(parentId);
        this.postDataAjaxStringJson(href, dataForm, onSuccess, onFailed);
    },
    
    postDataAjaxStringJson: function DataBindingScript_DataBinding$postDataAjaxStringJson(href, jsonStr, onSuccess, onFailed) {
        /// <param name="href" type="String">
        /// </param>
        /// <param name="jsonStr" type="String">
        /// </param>
        /// <param name="onSuccess" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        /// <param name="onFailed" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        jQuery.ajaxSetup({ cache: false });
        jQuery.ajax({ type: 'post', url: href, data: jsonStr, contentType: 'application/json;charset=utf-8', dataType: 'json', async: false, success: onSuccess, error: onFailed });
    },
    
    getData: function DataBindingScript_DataBinding$getData(href, parentId, onSuccess, onDeny) {
        /// <param name="href" type="String">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        /// <param name="onSuccess" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        /// <param name="onDeny" type="DataBindingScript.SimpleDelegate">
        /// </param>
        jQuery.ajaxSetup({ cache: false });
        jQuery('').ajaxError(Function.createDelegate(this, function(data1, data2, data3) {
            alert('error');
        }));
        jQuery.getJSON(href, {}, Function.createDelegate(this, function(data) {
            if (data === DataBindingScript.DataBinding.authenFail && onDeny != null) {
                onDeny();
            }
            this.bindData(data, parentId);
            if (onSuccess != null) {
                onSuccess(data);
            }
        }));
    },
    
    bindData: function DataBindingScript_DataBinding$bindData(dataJson, parentId) {
        /// <param name="dataJson" type="DataBindingScript.JsonResult">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        this._registerOnSuccessBindData();
        this._bindTextArea(dataJson, parentId);
        this._bindComboBox(dataJson, parentId);
        this._bindInput(dataJson, parentId);
        this._bindLabel(dataJson, parentId);
        this._raiseOnSuccessBindData();
    },
    
    _bindTextArea: function DataBindingScript_DataBinding$_bindTextArea(dataJson, parentId) {
        /// <param name="dataJson" type="DataBindingScript.JsonResult">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        var selector = ((parentId !== '') ? '#' : '') + parentId;
        selector += ' textarea[BindName]';
        var fields = jQuery(selector);
        var count = fields.length;
        for (var i = 0; i < count; i++) {
            var field = fields[i];
            var name = field.getAttribute('BindName');
            var format = field.getAttribute('DateFormat');
            if (dataJson[name] != null) {
                (field).value = (format == null || format === '') ? dataJson[name] : new Date(Number.parseInvariant((dataJson[name]).substring(6, (dataJson[name]).length - 2))).format(format);
            }
            DataBindingScript.DataBinding._setModelStatus(dataJson, field, name);
        }
    },
    
    _bindComboBox: function DataBindingScript_DataBinding$_bindComboBox(dataJson, parentId) {
        /// <param name="dataJson" type="DataBindingScript.JsonResult">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        var selector = ((parentId !== '') ? '#' : '') + parentId;
        selector += ' select[BindName]';
        var fields = jQuery(selector);
        var count = fields.length;
        for (var i = 0; i < count; i++) {
            var field = fields[i];
            var name = field.getAttribute('BindName');
            var src = field.getAttribute('BindSrc');
            if (src != null && src !== '') {
                this._registerOnSuccessBindData();
                jQuery.getJSON(src, {}, Function.createDelegate(this, function(data) {
                    var dataCombo = data;
                    var comboValue = field.getAttribute('BindValue');
                    var comboDesc = field.getAttribute('BindText');
                    var lengthItems = dataCombo.length;
                    if (lengthItems > 0) {
                        field.innerHTML = '';
                    }
                    for (var j = 0; j < lengthItems; j++) {
                        var dataItem = dataCombo[j];
                        var elOption = document.createElement('option');
                        elOption.setAttribute('value', dataItem[comboValue]);
                        if (dataItem[comboValue] === dataJson[name]) {
                            (elOption).checked = true;
                        }
                        elOption.innerHTML = dataItem[comboDesc];
                        field.appendChild(elOption);
                    }
                    this._raiseOnSuccessBindData();
                }));
            }
            else {
                var optElements = jQuery(selector + ' option');
                var lengthItem = optElements.length;
                for (var j = 0; j < lengthItem; j++) {
                    var elOption = optElements[j];
                    if (elOption.value === dataJson[name]) {
                        (elOption).checked = true;
                    }
                }
            }
            DataBindingScript.DataBinding._setModelStatus(dataJson, field, name);
        }
    },
    
    _bindInput: function DataBindingScript_DataBinding$_bindInput(dataJson, parentId) {
        /// <param name="dataJson" type="DataBindingScript.JsonResult">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        var selector = ((parentId !== '') ? '#' : '') + parentId;
        selector += ' input[BindName]';
        var fields = jQuery(selector);
        var count = fields.length;
        for (var i = 0; i < count; i++) {
            var field = fields[i];
            var name = field.getAttribute('BindName');
            var format = field.getAttribute('DateFormat');
            var check = false;
            var type = field.getAttribute('type');
            if (type == null) {
                type = 'text';
            }
            switch (type.toLowerCase()) {
                case '':
                case 'text':
                case 'hidden':
                case 'password':
                    if (dataJson[name] != null) {
                        (field).value = (format == null || format === '') ? dataJson[name] : new Date(Number.parseInvariant((dataJson[name]).substring(6, (dataJson[name]).length - 2))).format(format);
                    }
                    break;
                case 'checkbox':
                    check = dataJson[name];
                    (field).checked = check;
                    break;
                case 'image':
                    (field).src = dataJson[name];
                    break;
                case 'radio':
                    this._bindRadio(dataJson, field);
                    break;
                default:
                    break;
            }
            DataBindingScript.DataBinding._setModelStatus(dataJson, field, name);
        }
    },
    
    _bindRadio: function DataBindingScript_DataBinding$_bindRadio(dataJson, field) {
        /// <param name="dataJson" type="DataBindingScript.JsonResult">
        /// </param>
        /// <param name="field" type="Object" domElement="true">
        /// </param>
        var radioNameAtt = field.getAttribute('BindName');
        var nameRadio = field.getAttribute('popupId');
        var src = field.getAttribute('BindSrc');
        if (field.id !== '') {
            var labelField = jQuery(String.format('label[for=\"{0}\"', field.id));
            field.parentNode.removeChild(labelField[0]);
            var radioChild = jQuery(String.format('input[ChildOf=\"{0}\"]', field.id));
            for (var i = 0; i < radioChild.length; i++) {
                var idItem = radioChild[0].id;
                var lablesDel = jQuery(String.format('label[for=\"{0}\"', idItem));
                field.parentNode.removeChild(lablesDel[0]);
                field.parentNode.removeChild(radioChild[0]);
            }
        }
        if ((field).name == null || (field).name === '') {
            (field).name = 'jQName' + Math.random().toString().substring(2, 10);
        }
        if (src != null && src !== '') {
            this._registerOnSuccessBindData();
            jQuery.getJSON(src, {}, Function.createDelegate(this, function(data) {
                var dataRadio = data;
                var radioValue = field.getAttribute('BindValue');
                var radioDesc = field.getAttribute('BindText');
                var lengthItems = dataRadio.length;
                var parentField = field.parentNode;
                for (var i = 0; i < lengthItems - 1; i++) {
                    var dataItem = dataRadio[i];
                    var radioInput = document.createElement('input');
                    radioInput.setAttribute('type', 'radio');
                    radioInput.setAttribute('popupId', nameRadio);
                    radioInput.setAttribute('id', nameRadio + i.toString());
                    radioInput.setAttribute('ChildOf', nameRadio + (lengthItems - 1).toString());
                    radioInput.setAttribute('name', (field).name);
                    if (dataItem[radioValue] === dataJson[radioNameAtt]) {
                        (radioInput).checked = true;
                    }
                    (radioInput).value = dataItem[radioValue];
                    var labelField = document.createElement('label');
                    labelField.setAttribute('for', nameRadio + i.toString());
                    labelField.innerHTML = dataItem[radioDesc];
                    parentField.insertBefore(radioInput, field);
                    parentField.insertBefore(labelField, field);
                }
                if (lengthItems > 0) {
                    var dataItem = dataRadio[lengthItems - 1];
                    field.setAttribute('id', nameRadio + (lengthItems - 1).toString());
                    if (dataItem[radioValue] === dataJson[radioValue]) {
                        (field).checked = true;
                    }
                    (field).value = dataItem[radioValue];
                    var labelField = document.createElement('label');
                    labelField.setAttribute('for', nameRadio + (lengthItems - 1).toString());
                    labelField.innerText = dataItem[radioDesc];
                    if (parentField.lastChild === field) {
                        parentField.appendChild(labelField);
                    }
                    else {
                        var nextOfField = field.nextSibling;
                        parentField.insertBefore(labelField, nextOfField);
                    }
                }
                this._raiseOnSuccessBindData();
            }));
        }
        else {
            var radiosJQuery = jQuery(String.format('[name={0}]', (field).name));
            var count = radiosJQuery.length;
            for (var i = 0; i < count; i++) {
                var item = radiosJQuery[i];
                if (item.value === dataJson[radioNameAtt]) {
                    item.checked = true;
                }
            }
        }
    },
    
    _bindLabel: function DataBindingScript_DataBinding$_bindLabel(dataJson, parentId) {
        /// <param name="dataJson" type="DataBindingScript.JsonResult">
        /// </param>
        /// <param name="parentId" type="String">
        /// </param>
        var selector = ((parentId !== '') ? '#' : '') + parentId;
        selector += ' label[BindName]';
        var fields = jQuery(selector);
        var count = fields.length;
        for (var i = 0; i < count; i++) {
            var field = fields[i];
            var name = field.getAttribute('BindName');
            var format = field.getAttribute('DateFormat');
            if (dataJson[name] != null) {
                field.innerHTML = (format == null || format === '') ? dataJson[name] : new Date(Number.parseInvariant((dataJson[name]).substring(6, (dataJson[name]).length - 2))).format(format);
            }
            DataBindingScript.DataBinding._setModelStatus(dataJson, field, name);
        }
    },
    
    _getOnceDataJson: function DataBindingScript_DataBinding$_getOnceDataJson(href, cached, onSuccess) {
        /// <param name="href" type="String">
        /// </param>
        /// <param name="cached" type="Object">
        /// </param>
        /// <param name="onSuccess" type="DataBindingScript.GenericOneArgDelegate">
        /// </param>
        var dataJson = cached[href];
        if (dataJson == null) {
            this._registerOnSuccessBindData();
            jQuery.getJSON(href, {}, Function.createDelegate(this, function(data) {
                cached[href] = data;
                if (onSuccess != null) {
                    onSuccess(data);
                }
                this._raiseOnSuccessBindData();
            }));
        }
        else {
            if (onSuccess != null) {
                onSuccess(dataJson);
            }
        }
    },
    
    _queueBindDataComplete: 0,
    onSuccessBindData: null,
    
    _registerOnSuccessBindData: function DataBindingScript_DataBinding$_registerOnSuccessBindData() {
        this._queueBindDataComplete++;
    },
    
    _raiseOnSuccessBindData: function DataBindingScript_DataBinding$_raiseOnSuccessBindData() {
        this._queueBindDataComplete--;
        if (this._queueBindDataComplete === 0 && this.onSuccessBindData != null) {
            this.onSuccessBindData();
        }
    }
}


Type.registerNamespace('JQuerySharp');

DataBindingScript.JsonResult.registerClass('DataBindingScript.JsonResult');
DataBindingScript.DataRepeater.registerClass('DataBindingScript.DataRepeater');
DataBindingScript.DataGrid.registerClass('DataBindingScript.DataGrid');
DataBindingScript.DataBinding.registerClass('DataBindingScript.DataBinding');
DataBindingScript.DataBinding.authenFail = 0;

// ---- Do not remove this footer ----
// This script was generated using Script# v0.5.5.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
