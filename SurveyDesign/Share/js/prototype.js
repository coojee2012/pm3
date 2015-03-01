Object.extend = function(dest, source, replace) {
	for(var prop in source) {
		if(replace == false && dest[prop] != null) { continue; }
		dest[prop] = source[prop];
	}
	return dest;
};

Object.extend(Function.prototype, {
	apply: function(o, a) {
		var r, x = "__fapply";
		if(typeof o != "object") { o = {}; }
		o[x] = this;
		var s = "r = o." + x + "(";
		for(var i=0; i<a.length; i++) {
			if(i>0) { s += ","; }
			s += "a[" + i + "]";
		}
		s += ");";
		eval(s);
		delete o[x];
		return r;
	},
	bind: function(o) {
		if(!Function.__objs) {
			Function.__objs = [];
			Function.__funcs = [];
		}
		var objId = o.__oid;
		if(!objId) {
			Function.__objs[objId = o.__oid = Function.__objs.length] = o;
		}

		var me = this;
		var funcId = me.__fid;
		if(!funcId) {
			Function.__funcs[funcId = me.__fid = Function.__funcs.length] = me;
		}

		if(!o.__closures) {
			o.__closures = [];
		}

		var closure = o.__closures[funcId];
		if(closure) {
			return closure;
		}

		o = null;
		me = null;

		return Function.__objs[objId].__closures[funcId] = function() {
			return Function.__funcs[funcId].apply(Function.__objs[objId], arguments);
		};
	}
}, false);

Object.extend(Array.prototype, {
	push: function(o) {
		this[this.length] = o;
	},
	addRange: function(items) {
		if(items.length > 0) {
			for(var i=0; i<items.length; i++) {
				this.push(items[i]);
			}
		}
	},
	clear: function() {
		this.length = 0;
		return this;
	},
	shift: function() {
		if(this.length == 0) { return null; }
		var o = this[0];
		for(var i=0; i<this.length-1; i++) {
			this[i] = this[i + 1];
		}
		this.length--;
		return o;
	}
}, false);
if(typeof addEvent == "undefined")
	addEvent = function(o, evType, f, capture) {
		if(o == null) { return false; }
		if(o.addEventListener) {
			o.addEventListener(evType, f, capture);
			return true;
		} else if (o.attachEvent) {
			var r = o.attachEvent("on" + evType, f);
			return r;
		} else {
			try{ o["on" + evType] = f; }catch(e){}
		}
	};
	
if(typeof removeEvent == "undefined")
	removeEvent = function(o, evType, f, capture) {
		if(o == null) { return false; }
		if(o.removeEventListener) {
			o.removeEventListener(evType, f, capture);
			return true;
		} else if (o.detachEvent) {
			o.detachEvent("on" + evType, f);
		} else {
			try{ o["on" + evType] = function(){}; }catch(e){}
		}
	};