'use strict';

(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) : typeof define === 'function' && define.amd ? define('GOVUKFrontend', ['exports'], factory) : factory(global.GOVUKFrontend = {});
})(undefined, function (exports) {
  'use strict';

  /**
   * TODO: Ideally this would be a NodeList.prototype.forEach polyfill
   * This seems to fail in IE8, requires more investigation.
   * See: https://github.com/imagitama/nodelist-foreach-polyfill
   */
  function nodeListForEach(nodes, callback) {
    if (window.NodeList.prototype.forEach) {
      return nodes.forEach(callback);
    }
    for (var i = 0; i < nodes.length; i++) {
      callback.call(window, nodes[i], i, nodes);
    }
  }

  // Used to generate a unique string, allows multiple instances of the component without
  // Them conflicting with each other.
  // https://stackoverflow.com/a/8809472
  function generateUniqueID() {
    var d = new Date().getTime();
    if (typeof window.performance !== 'undefined' && typeof window.performance.now === 'function') {
      d += window.performance.now(); // use high-precision timer if available
    }
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = (d + Math.random() * 16) % 16 | 0;
      d = Math.floor(d / 16);
      return (c === 'x' ? r : r & 0x3 | 0x8).toString(16);
    });
  }

  (function (undefined) {

    // Detection from https://github.com/Financial-Times/polyfill-service/blob/master/packages/polyfill-library/polyfills/Object/defineProperty/detect.js
    var detect =
    // In IE8, defineProperty could only act on DOM elements, so full support
    // for the feature requires the ability to set a property on an arbitrary object
    'defineProperty' in Object && (function () {
      try {
        var a = {};
        Object.defineProperty(a, 'test', { value: 42 });
        return true;
      } catch (e) {
        return false;
      }
    })();

    if (detect) return;

    // Polyfill from https://cdn.polyfill.io/v2/polyfill.js?features=Object.defineProperty&flags=always
    (function (nativeDefineProperty) {

      var supportsAccessors = Object.prototype.hasOwnProperty('__defineGetter__');
      var ERR_ACCESSORS_NOT_SUPPORTED = 'Getters & setters cannot be defined on this javascript engine';
      var ERR_VALUE_ACCESSORS = 'A property cannot both have accessors and be writable or have a value';

      Object.defineProperty = function defineProperty(object, property, descriptor) {

        // Where native support exists, assume it
        if (nativeDefineProperty && (object === window || object === document || object === Element.prototype || object instanceof Element)) {
          return nativeDefineProperty(object, property, descriptor);
        }

        if (object === null || !(object instanceof Object || typeof object === 'object')) {
          throw new TypeError('Object.defineProperty called on non-object');
        }

        if (!(descriptor instanceof Object)) {
          throw new TypeError('Property description must be an object');
        }

        var propertyString = String(property);
        var hasValueOrWritable = 'value' in descriptor || 'writable' in descriptor;
        var getterType = 'get' in descriptor && typeof descriptor.get;
        var setterType = 'set' in descriptor && typeof descriptor.set;

        // handle descriptor.get
        if (getterType) {
          if (getterType !== 'function') {
            throw new TypeError('Getter must be a function');
          }
          if (!supportsAccessors) {
            throw new TypeError(ERR_ACCESSORS_NOT_SUPPORTED);
          }
          if (hasValueOrWritable) {
            throw new TypeError(ERR_VALUE_ACCESSORS);
          }
          Object.__defineGetter__.call(object, propertyString, descriptor.get);
        } else {
          object[propertyString] = descriptor.value;
        }

        // handle descriptor.set
        if (setterType) {
          if (setterType !== 'function') {
            throw new TypeError('Setter must be a function');
          }
          if (!supportsAccessors) {
            throw new TypeError(ERR_ACCESSORS_NOT_SUPPORTED);
          }
          if (hasValueOrWritable) {
            throw new TypeError(ERR_VALUE_ACCESSORS);
          }
          Object.__defineSetter__.call(object, propertyString, descriptor.set);
        }

        // OK to define value unconditionally - if a getter has been specified as well, an error would be thrown above
        if ('value' in descriptor) {
          object[propertyString] = descriptor.value;
        }

        return object;
      };
    })(Object.defineProperty);
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {
    // Detection from https://github.com/Financial-Times/polyfill-service/blob/master/packages/polyfill-library/polyfills/Function/prototype/bind/detect.js
    var detect = ('bind' in Function.prototype);

    if (detect) return;

    // Polyfill from https://cdn.polyfill.io/v2/polyfill.js?features=Function.prototype.bind&flags=always
    Object.defineProperty(Function.prototype, 'bind', {
      value: function bind(that) {
        // .length is 1
        // add necessary es5-shim utilities
        var $Array = Array;
        var $Object = Object;
        var ObjectPrototype = $Object.prototype;
        var ArrayPrototype = $Array.prototype;
        var Empty = function Empty() {};
        var to_string = ObjectPrototype.toString;
        var hasToStringTag = typeof Symbol === 'function' && typeof Symbol.toStringTag === 'symbol';
        var isCallable; /* inlined from https://npmjs.com/is-callable */var fnToStr = Function.prototype.toString,
            tryFunctionObject = function tryFunctionObject(value) {
          try {
            fnToStr.call(value);return true;
          } catch (e) {
            return false;
          }
        },
            fnClass = '[object Function]',
            genClass = '[object GeneratorFunction]';isCallable = function isCallable(value) {
          if (typeof value !== 'function') {
            return false;
          }if (hasToStringTag) {
            return tryFunctionObject(value);
          }var strClass = to_string.call(value);return strClass === fnClass || strClass === genClass;
        };
        var array_slice = ArrayPrototype.slice;
        var array_concat = ArrayPrototype.concat;
        var array_push = ArrayPrototype.push;
        var max = Math.max;
        // /add necessary es5-shim utilities

        // 1. Let Target be the this value.
        var target = this;
        // 2. If IsCallable(Target) is false, throw a TypeError exception.
        if (!isCallable(target)) {
          throw new TypeError('Function.prototype.bind called on incompatible ' + target);
        }
        // 3. Let A be a new (possibly empty) internal list of all of the
        //   argument values provided after thisArg (arg1, arg2 etc), in order.
        // XXX slicedArgs will stand in for "A" if used
        var args = array_slice.call(arguments, 1); // for normal call
        // 4. Let F be a new native ECMAScript object.
        // 11. Set the [[Prototype]] internal property of F to the standard
        //   built-in Function prototype object as specified in 15.3.3.1.
        // 12. Set the [[Call]] internal property of F as described in
        //   15.3.4.5.1.
        // 13. Set the [[Construct]] internal property of F as described in
        //   15.3.4.5.2.
        // 14. Set the [[HasInstance]] internal property of F as described in
        //   15.3.4.5.3.
        var bound;
        var binder = function binder() {

          if (this instanceof bound) {
            // 15.3.4.5.2 [[Construct]]
            // When the [[Construct]] internal method of a function object,
            // F that was created using the bind function is called with a
            // list of arguments ExtraArgs, the following steps are taken:
            // 1. Let target be the value of F's [[TargetFunction]]
            //   internal property.
            // 2. If target has no [[Construct]] internal method, a
            //   TypeError exception is thrown.
            // 3. Let boundArgs be the value of F's [[BoundArgs]] internal
            //   property.
            // 4. Let args be a new list containing the same values as the
            //   list boundArgs in the same order followed by the same
            //   values as the list ExtraArgs in the same order.
            // 5. Return the result of calling the [[Construct]] internal
            //   method of target providing args as the arguments.

            var result = target.apply(this, array_concat.call(args, array_slice.call(arguments)));
            if ($Object(result) === result) {
              return result;
            }
            return this;
          } else {
            // 15.3.4.5.1 [[Call]]
            // When the [[Call]] internal method of a function object, F,
            // which was created using the bind function is called with a
            // this value and a list of arguments ExtraArgs, the following
            // steps are taken:
            // 1. Let boundArgs be the value of F's [[BoundArgs]] internal
            //   property.
            // 2. Let boundThis be the value of F's [[BoundThis]] internal
            //   property.
            // 3. Let target be the value of F's [[TargetFunction]] internal
            //   property.
            // 4. Let args be a new list containing the same values as the
            //   list boundArgs in the same order followed by the same
            //   values as the list ExtraArgs in the same order.
            // 5. Return the result of calling the [[Call]] internal method
            //   of target providing boundThis as the this value and
            //   providing args as the arguments.

            // equiv: target.call(this, ...boundArgs, ...args)
            return target.apply(that, array_concat.call(args, array_slice.call(arguments)));
          }
        };

        // 15. If the [[Class]] internal property of Target is "Function", then
        //     a. Let L be the length property of Target minus the length of A.
        //     b. Set the length own property of F to either 0 or L, whichever is
        //       larger.
        // 16. Else set the length own property of F to 0.

        var boundLength = max(0, target.length - args.length);

        // 17. Set the attributes of the length own property of F to the values
        //   specified in 15.3.5.1.
        var boundArgs = [];
        for (var i = 0; i < boundLength; i++) {
          array_push.call(boundArgs, '$' + i);
        }

        // XXX Build a dynamic function with desired amount of arguments is the only
        // way to set the length property of a function.
        // In environments where Content Security Policies enabled (Chrome extensions,
        // for ex.) all use of eval or Function costructor throws an exception.
        // However in all of these environments Function.prototype.bind exists
        // and so this code will never be executed.
        bound = Function('binder', 'return function (' + boundArgs.join(',') + '){ return binder.apply(this, arguments); }')(binder);

        if (target.prototype) {
          Empty.prototype = target.prototype;
          bound.prototype = new Empty();
          // Clean up dangling references.
          Empty.prototype = null;
        }

        // TODO
        // 18. Set the [[Extensible]] internal property of F to true.

        // TODO
        // 19. Let thrower be the [[ThrowTypeError]] function Object (13.2.3).
        // 20. Call the [[DefineOwnProperty]] internal method of F with
        //   arguments "caller", PropertyDescriptor {[[Get]]: thrower, [[Set]]:
        //   thrower, [[Enumerable]]: false, [[Configurable]]: false}, and
        //   false.
        // 21. Call the [[DefineOwnProperty]] internal method of F with
        //   arguments "arguments", PropertyDescriptor {[[Get]]: thrower,
        //   [[Set]]: thrower, [[Enumerable]]: false, [[Configurable]]: false},
        //   and false.

        // TODO
        // NOTE Function objects created using Function.prototype.bind do not
        // have a prototype property or the [[Code]], [[FormalParameters]], and
        // [[Scope]] internal properties.
        // XXX can't delete prototype in pure-js.

        // 22. Return F.
        return bound;
      }
    });
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {

    // Detection from https://raw.githubusercontent.com/Financial-Times/polyfill-service/master/packages/polyfill-library/polyfills/DOMTokenList/detect.js
    var detect = 'DOMTokenList' in this && (function (x) {
      return 'classList' in x ? !x.classList.toggle('x', false) && !x.className : true;
    })(document.createElement('x'));

    if (detect) return;

    // Polyfill from https://raw.githubusercontent.com/Financial-Times/polyfill-service/master/packages/polyfill-library/polyfills/DOMTokenList/polyfill.js
    (function (global) {
      var nativeImpl = "DOMTokenList" in global && global.DOMTokenList;

      if (!nativeImpl || !!document.createElementNS && !!document.createElementNS('http://www.w3.org/2000/svg', 'svg') && !(document.createElementNS("http://www.w3.org/2000/svg", "svg").classList instanceof DOMTokenList)) {
        global.DOMTokenList = (function () {
          // eslint-disable-line no-unused-vars
          var dpSupport = true;
          var defineGetter = function defineGetter(object, name, fn, configurable) {
            if (Object.defineProperty) Object.defineProperty(object, name, {
              configurable: false === dpSupport ? true : !!configurable,
              get: fn
            });else object.__defineGetter__(name, fn);
          };

          /** Ensure the browser allows Object.defineProperty to be used on native JavaScript objects. */
          try {
            defineGetter({}, "support");
          } catch (e) {
            dpSupport = false;
          }

          var _DOMTokenList = function _DOMTokenList(el, prop) {
            var that = this;
            var tokens = [];
            var tokenMap = {};
            var length = 0;
            var maxLength = 0;
            var addIndexGetter = function addIndexGetter(i) {
              defineGetter(that, i, function () {
                preop();
                return tokens[i];
              }, false);
            };
            var reindex = function reindex() {

              /** Define getter functions for array-like access to the tokenList's contents. */
              if (length >= maxLength) for (; maxLength < length; ++maxLength) {
                addIndexGetter(maxLength);
              }
            };

            /** Helper function called at the start of each class method. Internal use only. */
            var preop = function preop() {
              var error;
              var i;
              var args = arguments;
              var rSpace = /\s+/;

              /** Validate the token/s passed to an instance method, if any. */
              if (args.length) for (i = 0; i < args.length; ++i) if (rSpace.test(args[i])) {
                error = new SyntaxError('String "' + args[i] + '" ' + "contains" + ' an invalid character');
                error.code = 5;
                error.name = "InvalidCharacterError";
                throw error;
              }

              /** Split the new value apart by whitespace*/
              if (typeof el[prop] === "object") {
                tokens = ("" + el[prop].baseVal).replace(/^\s+|\s+$/g, "").split(rSpace);
              } else {
                tokens = ("" + el[prop]).replace(/^\s+|\s+$/g, "").split(rSpace);
              }

              /** Avoid treating blank strings as single-item token lists */
              if ("" === tokens[0]) tokens = [];

              /** Repopulate the internal token lists */
              tokenMap = {};
              for (i = 0; i < tokens.length; ++i) tokenMap[tokens[i]] = true;
              length = tokens.length;
              reindex();
            };

            /** Populate our internal token list if the targeted attribute of the subject element isn't empty. */
            preop();

            /** Return the number of tokens in the underlying string. Read-only. */
            defineGetter(that, "length", function () {
              preop();
              return length;
            });

            /** Override the default toString/toLocaleString methods to return a space-delimited list of tokens when typecast. */
            that.toLocaleString = that.toString = function () {
              preop();
              return tokens.join(" ");
            };

            that.item = function (idx) {
              preop();
              return tokens[idx];
            };

            that.contains = function (token) {
              preop();
              return !!tokenMap[token];
            };

            that.add = function () {
              preop.apply(that, args = arguments);

              for (var args, token, i = 0, l = args.length; i < l; ++i) {
                token = args[i];
                if (!tokenMap[token]) {
                  tokens.push(token);
                  tokenMap[token] = true;
                }
              }

              /** Update the targeted attribute of the attached element if the token list's changed. */
              if (length !== tokens.length) {
                length = tokens.length >>> 0;
                if (typeof el[prop] === "object") {
                  el[prop].baseVal = tokens.join(" ");
                } else {
                  el[prop] = tokens.join(" ");
                }
                reindex();
              }
            };

            that.remove = function () {
              preop.apply(that, args = arguments);

              /** Build a hash of token names to compare against when recollecting our token list. */
              for (var args, ignore = {}, i = 0, t = []; i < args.length; ++i) {
                ignore[args[i]] = true;
                delete tokenMap[args[i]];
              }

              /** Run through our tokens list and reassign only those that aren't defined in the hash declared above. */
              for (i = 0; i < tokens.length; ++i) if (!ignore[tokens[i]]) t.push(tokens[i]);

              tokens = t;
              length = t.length >>> 0;

              /** Update the targeted attribute of the attached element. */
              if (typeof el[prop] === "object") {
                el[prop].baseVal = tokens.join(" ");
              } else {
                el[prop] = tokens.join(" ");
              }
              reindex();
            };

            that.toggle = function (token, force) {
              preop.apply(that, [token]);

              /** Token state's being forced. */
              if (undefined !== force) {
                if (force) {
                  that.add(token);
                  return true;
                } else {
                  that.remove(token);
                  return false;
                }
              }

              /** Token already exists in tokenList. Remove it, and return FALSE. */
              if (tokenMap[token]) {
                that.remove(token);
                return false;
              }

              /** Otherwise, add the token and return TRUE. */
              that.add(token);
              return true;
            };

            return that;
          };

          return _DOMTokenList;
        })();
      }

      // Add second argument to native DOMTokenList.toggle() if necessary
      (function () {
        var e = document.createElement('span');
        if (!('classList' in e)) return;
        e.classList.toggle('x', false);
        if (!e.classList.contains('x')) return;
        e.classList.constructor.prototype.toggle = function toggle(token /*, force*/) {
          var force = arguments[1];
          if (force === undefined) {
            var add = !this.contains(token);
            this[add ? 'add' : 'remove'](token);
            return add;
          }
          force = !!force;
          this[force ? 'add' : 'remove'](token);
          return force;
        };
      })();

      // Add multiple arguments to native DOMTokenList.add() if necessary
      (function () {
        var e = document.createElement('span');
        if (!('classList' in e)) return;
        e.classList.add('a', 'b');
        if (e.classList.contains('b')) return;
        var native = e.classList.constructor.prototype.add;
        e.classList.constructor.prototype.add = function () {
          var args = arguments;
          var l = arguments.length;
          for (var i = 0; i < l; i++) {
            native.call(this, args[i]);
          }
        };
      })();

      // Add multiple arguments to native DOMTokenList.remove() if necessary
      (function () {
        var e = document.createElement('span');
        if (!('classList' in e)) return;
        e.classList.add('a');
        e.classList.add('b');
        e.classList.remove('a', 'b');
        if (!e.classList.contains('b')) return;
        var native = e.classList.constructor.prototype.remove;
        e.classList.constructor.prototype.remove = function () {
          var args = arguments;
          var l = arguments.length;
          for (var i = 0; i < l; i++) {
            native.call(this, args[i]);
          }
        };
      })();
    })(this);
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {

    // Detection from https://github.com/Financial-Times/polyfill-service/blob/master/packages/polyfill-library/polyfills/Document/detect.js
    var detect = ("Document" in this);

    if (detect) return;

    // Polyfill from https://cdn.polyfill.io/v2/polyfill.js?features=Document&flags=always
    if (typeof WorkerGlobalScope === "undefined" && typeof importScripts !== "function") {

      if (this.HTMLDocument) {
        // IE8

        // HTMLDocument is an extension of Document.  If the browser has HTMLDocument but not Document, the former will suffice as an alias for the latter.
        this.Document = this.HTMLDocument;
      } else {

        // Create an empty function to act as the missing constructor for the document object, attach the document object as its prototype.  The function needs to be anonymous else it is hoisted and causes the feature detect to prematurely pass, preventing the assignments below being made.
        this.Document = this.HTMLDocument = document.constructor = new Function('return function Document() {}')();
        this.Document.prototype = document;
      }
    }
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {

    // Detection from https://github.com/Financial-Times/polyfill-service/blob/master/packages/polyfill-library/polyfills/Element/detect.js
    var detect = 'Element' in this && 'HTMLElement' in this;

    if (detect) return;

    // Polyfill from https://cdn.polyfill.io/v2/polyfill.js?features=Element&flags=always
    (function () {

      // IE8
      if (window.Element && !window.HTMLElement) {
        window.HTMLElement = window.Element;
        return;
      }

      // create Element constructor
      window.Element = window.HTMLElement = new Function('return function Element() {}')();

      // generate sandboxed iframe
      var vbody = document.appendChild(document.createElement('body'));
      var frame = vbody.appendChild(document.createElement('iframe'));

      // use sandboxed iframe to replicate Element functionality
      var frameDocument = frame.contentWindow.document;
      var prototype = Element.prototype = frameDocument.appendChild(frameDocument.createElement('*'));
      var cache = {};

      // polyfill Element.prototype on an element
      var shiv = function shiv(element, deep) {
        var childNodes = element.childNodes || [],
            index = -1,
            key,
            value,
            childNode;

        if (element.nodeType === 1 && element.constructor !== Element) {
          element.constructor = Element;

          for (key in cache) {
            value = cache[key];
            element[key] = value;
          }
        }

        while (childNode = deep && childNodes[++index]) {
          shiv(childNode, deep);
        }

        return element;
      };

      var elements = document.getElementsByTagName('*');
      var nativeCreateElement = document.createElement;
      var interval;
      var loopLimit = 100;

      prototype.attachEvent('onpropertychange', function (event) {
        var propertyName = event.propertyName,
            nonValue = !cache.hasOwnProperty(propertyName),
            newValue = prototype[propertyName],
            oldValue = cache[propertyName],
            index = -1,
            element;

        while (element = elements[++index]) {
          if (element.nodeType === 1) {
            if (nonValue || element[propertyName] === oldValue) {
              element[propertyName] = newValue;
            }
          }
        }

        cache[propertyName] = newValue;
      });

      prototype.constructor = Element;

      if (!prototype.hasAttribute) {
        // <Element>.hasAttribute
        prototype.hasAttribute = function hasAttribute(name) {
          return this.getAttribute(name) !== null;
        };
      }

      // Apply Element prototype to the pre-existing DOM as soon as the body element appears.
      function bodyCheck() {
        if (! loopLimit--) clearTimeout(interval);
        if (document.body && !document.body.prototype && /(complete|interactive)/.test(document.readyState)) {
          shiv(document, true);
          if (interval && document.body.prototype) clearTimeout(interval);
          return !!document.body.prototype;
        }
        return false;
      }
      if (!bodyCheck()) {
        document.onreadystatechange = bodyCheck;
        interval = setInterval(bodyCheck, 25);
      }

      // Apply to any new elements created after load
      document.createElement = function createElement(nodeName) {
        var element = nativeCreateElement(String(nodeName).toLowerCase());
        return shiv(element);
      };

      // remove sandboxed iframe
      document.removeChild(vbody);
    })();
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {

    // Detection from https://raw.githubusercontent.com/Financial-Times/polyfill-service/8717a9e04ac7aff99b4980fbedead98036b0929a/packages/polyfill-library/polyfills/Element/prototype/classList/detect.js
    var detect = 'document' in this && "classList" in document.documentElement && 'Element' in this && 'classList' in Element.prototype && (function () {
      var e = document.createElement('span');
      e.classList.add('a', 'b');
      return e.classList.contains('b');
    })();

    if (detect) return;

    // Polyfill from https://cdn.polyfill.io/v2/polyfill.js?features=Element.prototype.classList&flags=always
    (function (global) {
      var dpSupport = true;
      var defineGetter = function defineGetter(object, name, fn, configurable) {
        if (Object.defineProperty) Object.defineProperty(object, name, {
          configurable: false === dpSupport ? true : !!configurable,
          get: fn
        });else object.__defineGetter__(name, fn);
      };
      /** Ensure the browser allows Object.defineProperty to be used on native JavaScript objects. */
      try {
        defineGetter({}, "support");
      } catch (e) {
        dpSupport = false;
      }
      /** Polyfills a property with a DOMTokenList */
      var addProp = function addProp(o, name, attr) {

        defineGetter(o.prototype, name, function () {
          var tokenList;

          var THIS = this,

          /** Prevent this from firing twice for some reason. What the hell, IE. */
          gibberishProperty = "__defineGetter__" + "DEFINE_PROPERTY" + name;
          if (THIS[gibberishProperty]) return tokenList;
          THIS[gibberishProperty] = true;

          /**
           * IE8 can't define properties on native JavaScript objects, so we'll use a dumb hack instead.
           *
           * What this is doing is creating a dummy element ("reflection") inside a detached phantom node ("mirror")
           * that serves as the target of Object.defineProperty instead. While we could simply use the subject HTML
           * element instead, this would conflict with element types which use indexed properties (such as forms and
           * select lists).
           */
          if (false === dpSupport) {

            var visage;
            var mirror = addProp.mirror || document.createElement("div");
            var reflections = mirror.childNodes;
            var l = reflections.length;

            for (var i = 0; i < l; ++i) if (reflections[i]._R === THIS) {
              visage = reflections[i];
              break;
            }

            /** Couldn't find an element's reflection inside the mirror. Materialise one. */
            visage || (visage = mirror.appendChild(document.createElement("div")));

            tokenList = DOMTokenList.call(visage, THIS, attr);
          } else tokenList = new DOMTokenList(THIS, attr);

          defineGetter(THIS, name, function () {
            return tokenList;
          });
          delete THIS[gibberishProperty];

          return tokenList;
        }, true);
      };

      addProp(global.Element, "classList", "className");
      addProp(global.HTMLElement, "classList", "className");
      addProp(global.HTMLLinkElement, "relList", "rel");
      addProp(global.HTMLAnchorElement, "relList", "rel");
      addProp(global.HTMLAreaElement, "relList", "rel");
    })(this);
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  function Accordion($module) {
    this.$module = $module;
    this.moduleId = $module.getAttribute('id');
    this.$sections = $module.querySelectorAll('.govuk-accordion__section');
    this.$openAllButton = '';
    this.browserSupportsSessionStorage = helper.checkForSessionStorage();

    this.controlsClass = 'govuk-accordion__controls';
    this.openAllClass = 'govuk-accordion__open-all';
    this.iconClass = 'govuk-accordion__icon';

    this.sectionHeaderClass = 'govuk-accordion__section-header';
    this.sectionHeaderFocusedClass = 'govuk-accordion__section-header--focused';
    this.sectionHeadingClass = 'govuk-accordion__section-heading';
    this.sectionSummaryClass = 'govuk-accordion__section-summary';
    this.sectionButtonClass = 'govuk-accordion__section-button';
    this.sectionExpandedClass = 'govuk-accordion__section--expanded';
  }

  // Initialize component
  Accordion.prototype.init = function () {
    // Check for module
    if (!this.$module) {
      return;
    }

    this.initControls();

    this.initSectionHeaders();

    // See if "Open all" button text should be updated
    var areAllSectionsOpen = this.checkIfAllSectionsOpen();
    this.updateOpenAllButton(areAllSectionsOpen);
  };

  // Initialise controls and set attributes
  Accordion.prototype.initControls = function () {
    // Create "Open all" button and set attributes
    this.$openAllButton = document.createElement('button');
    this.$openAllButton.setAttribute('type', 'button');
    this.$openAllButton.innerHTML = 'Open all <span class="govuk-visually-hidden">sections</span>';
    this.$openAllButton.setAttribute('class', this.openAllClass);
    this.$openAllButton.setAttribute('aria-expanded', 'false');
    this.$openAllButton.setAttribute('type', 'button');

    // Create control wrapper and add controls to it
    var accordionControls = document.createElement('div');
    accordionControls.setAttribute('class', this.controlsClass);
    accordionControls.appendChild(this.$openAllButton);
    this.$module.insertBefore(accordionControls, this.$module.firstChild);

    // Handle events for the controls
    this.$openAllButton.addEventListener('click', this.onOpenOrCloseAllToggle.bind(this));
  };

  // Initialise section headers
  Accordion.prototype.initSectionHeaders = function () {
    // Loop through section headers
    nodeListForEach(this.$sections, (function ($section, i) {
      // Set header attributes
      var header = $section.querySelector('.' + this.sectionHeaderClass);
      this.initHeaderAttributes(header, i);

      this.setExpanded(this.isExpanded($section), $section);

      // Handle events
      header.addEventListener('click', this.onSectionToggle.bind(this, $section));

      // See if there is any state stored in sessionStorage and set the sections to
      // open or closed.
      this.setInitialState($section);
    }).bind(this));
  };

  // Set individual header attributes
  Accordion.prototype.initHeaderAttributes = function ($headerWrapper, index) {
    var $module = this;
    var $span = $headerWrapper.querySelector('.' + this.sectionButtonClass);
    var $heading = $headerWrapper.querySelector('.' + this.sectionHeadingClass);
    var $summary = $headerWrapper.querySelector('.' + this.sectionSummaryClass);

    // Copy existing span element to an actual button element, for improved accessibility.
    var $button = document.createElement('button');
    $button.setAttribute('type', 'button');
    $button.setAttribute('id', this.moduleId + '-heading-' + (index + 1));
    $button.setAttribute('aria-controls', this.moduleId + '-content-' + (index + 1));

    // Copy all attributes (https://developer.mozilla.org/en-US/docs/Web/API/Element/attributes) from $span to $button
    for (var i = 0; i < $span.attributes.length; i++) {
      var attr = $span.attributes.item(i);
      $button.setAttribute(attr.nodeName, attr.nodeValue);
    }

    $button.addEventListener('focusin', function (e) {
      if (!$headerWrapper.classList.contains($module.sectionHeaderFocusedClass)) {
        $headerWrapper.className += ' ' + $module.sectionHeaderFocusedClass;
      }
    });

    $button.addEventListener('blur', function (e) {
      $headerWrapper.classList.remove($module.sectionHeaderFocusedClass);
    });

    if (typeof $summary !== 'undefined' && $summary !== null) {
      $button.setAttribute('aria-describedby', this.moduleId + '-summary-' + (index + 1));
    }

    // $span could contain HTML elements (see https://www.w3.org/TR/2011/WD-html5-20110525/content-models.html#phrasing-content)
    $button.innerHTML = $span.innerHTML;

    $heading.removeChild($span);
    $heading.appendChild($button);

    // Add "+/-" icon
    var icon = document.createElement('span');
    icon.className = this.iconClass;
    icon.setAttribute('aria-hidden', 'true');

    $heading.appendChild(icon);
  };

  // When section toggled, set and store state
  Accordion.prototype.onSectionToggle = function ($section) {
    var expanded = this.isExpanded($section);
    this.setExpanded(!expanded, $section);

    // Store the state in sessionStorage when a change is triggered
    this.storeState($section);
  };

  // When Open/Close All toggled, set and store state
  Accordion.prototype.onOpenOrCloseAllToggle = function () {
    var $module = this;
    var $sections = this.$sections;

    var nowExpanded = !this.checkIfAllSectionsOpen();

    nodeListForEach($sections, function ($section) {
      $module.setExpanded(nowExpanded, $section);
      // Store the state in sessionStorage when a change is triggered
      $module.storeState($section);
    });

    $module.updateOpenAllButton(nowExpanded);
  };

  // Set section attributes when opened/closed
  Accordion.prototype.setExpanded = function (expanded, $section) {
    var $button = $section.querySelector('.' + this.sectionButtonClass);
    $button.setAttribute('aria-expanded', expanded);

    if (expanded) {
      $section.classList.add(this.sectionExpandedClass);
    } else {
      $section.classList.remove(this.sectionExpandedClass);
    }

    // See if "Open all" button text should be updated
    var areAllSectionsOpen = this.checkIfAllSectionsOpen();
    this.updateOpenAllButton(areAllSectionsOpen);
  };

  // Get state of section
  Accordion.prototype.isExpanded = function ($section) {
    return $section.classList.contains(this.sectionExpandedClass);
  };

  // Check if all sections are open
  Accordion.prototype.checkIfAllSectionsOpen = function () {
    // Get a count of all the Accordion sections
    var sectionsCount = this.$sections.length;
    // Get a count of all Accordion sections that are expanded
    var expandedSectionCount = this.$module.querySelectorAll('.' + this.sectionExpandedClass).length;
    var areAllSectionsOpen = sectionsCount === expandedSectionCount;

    return areAllSectionsOpen;
  };

  // Update "Open all" button
  Accordion.prototype.updateOpenAllButton = function (expanded) {
    var newButtonText = expanded ? 'Close all' : 'Open all';
    newButtonText += '<span class="govuk-visually-hidden"> sections</span>';
    this.$openAllButton.setAttribute('aria-expanded', expanded);
    this.$openAllButton.innerHTML = newButtonText;
  };

  // Check for `window.sessionStorage`, and that it actually works.
  var helper = {
    checkForSessionStorage: function checkForSessionStorage() {
      var testString = 'this is the test string';
      var result;
      try {
        window.sessionStorage.setItem(testString, testString);
        result = window.sessionStorage.getItem(testString) === testString.toString();
        window.sessionStorage.removeItem(testString);
        return result;
      } catch (exception) {
        if (typeof console === 'undefined' || typeof console.log === 'undefined') {
          console.log('Notice: sessionStorage not available.');
        }
      }
    }
  };

  // Set the state of the accordions in sessionStorage
  Accordion.prototype.storeState = function ($section) {
    if (this.browserSupportsSessionStorage) {
      // We need a unique way of identifying each content in the accordion. Since
      // an `#id` should be unique and an `id` is required for `aria-` attributes
      // `id` can be safely used.
      var $button = $section.querySelector('.' + this.sectionButtonClass);

      if ($button) {
        var contentId = $button.getAttribute('aria-controls');
        var contentState = $button.getAttribute('aria-expanded');

        if (typeof contentId === 'undefined' && (typeof console === 'undefined' || typeof console.log === 'undefined')) {
          console.error(new Error('No aria controls present in accordion section heading.'));
        }

        if (typeof contentState === 'undefined' && (typeof console === 'undefined' || typeof console.log === 'undefined')) {
          console.error(new Error('No aria expanded present in accordion section heading.'));
        }

        // Only set the state when both `contentId` and `contentState` are taken from the DOM.
        if (contentId && contentState) {
          window.sessionStorage.setItem(contentId, contentState);
        }
      }
    }
  };

  // Read the state of the accordions from sessionStorage
  Accordion.prototype.setInitialState = function ($section) {
    if (this.browserSupportsSessionStorage) {
      var $button = $section.querySelector('.' + this.sectionButtonClass);

      if ($button) {
        var contentId = $button.getAttribute('aria-controls');
        var contentState = contentId ? window.sessionStorage.getItem(contentId) : null;

        if (contentState !== null) {
          this.setExpanded(contentState === 'true', $section);
        }
      }
    }
  };

  (function (undefined) {

    // Detection from https://github.com/Financial-Times/polyfill-service/blob/master/packages/polyfill-library/polyfills/Window/detect.js
    var detect = ('Window' in this);

    if (detect) return;

    // Polyfill from https://cdn.polyfill.io/v2/polyfill.js?features=Window&flags=always
    if (typeof WorkerGlobalScope === "undefined" && typeof importScripts !== "function") {
      (function (global) {
        if (global.constructor) {
          global.Window = global.constructor;
        } else {
          (global.Window = global.constructor = new Function('return function Window() {}')()).prototype = this;
        }
      })(this);
    }
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {

    // Detection from https://github.com/Financial-Times/polyfill-service/blob/master/packages/polyfill-library/polyfills/Event/detect.js
    var detect = (function (global) {

      if (!('Event' in global)) return false;
      if (typeof global.Event === 'function') return true;

      try {

        // In IE 9-11, the Event object exists but cannot be instantiated
        new Event('click');
        return true;
      } catch (e) {
        return false;
      }
    })(this);

    if (detect) return;

    // Polyfill from https://cdn.polyfill.io/v2/polyfill.js?features=Event&flags=always
    (function () {
      var unlistenableWindowEvents = {
        click: 1,
        dblclick: 1,
        keyup: 1,
        keypress: 1,
        keydown: 1,
        mousedown: 1,
        mouseup: 1,
        mousemove: 1,
        mouseover: 1,
        mouseenter: 1,
        mouseleave: 1,
        mouseout: 1,
        storage: 1,
        storagecommit: 1,
        textinput: 1
      };

      // This polyfill depends on availability of `document` so will not run in a worker
      // However, we asssume there are no browsers with worker support that lack proper
      // support for `Event` within the worker
      if (typeof document === 'undefined' || typeof window === 'undefined') return;

      function indexOf(array, element) {
        var index = -1,
            length = array.length;

        while (++index < length) {
          if (index in array && array[index] === element) {
            return index;
          }
        }

        return -1;
      }

      var existingProto = window.Event && window.Event.prototype || null;
      window.Event = Window.prototype.Event = function Event(type, eventInitDict) {
        if (!type) {
          throw new Error('Not enough arguments');
        }

        var event;
        // Shortcut if browser supports createEvent
        if ('createEvent' in document) {
          event = document.createEvent('Event');
          var bubbles = eventInitDict && eventInitDict.bubbles !== undefined ? eventInitDict.bubbles : false;
          var cancelable = eventInitDict && eventInitDict.cancelable !== undefined ? eventInitDict.cancelable : false;

          event.initEvent(type, bubbles, cancelable);

          return event;
        }

        event = document.createEventObject();

        event.type = type;
        event.bubbles = eventInitDict && eventInitDict.bubbles !== undefined ? eventInitDict.bubbles : false;
        event.cancelable = eventInitDict && eventInitDict.cancelable !== undefined ? eventInitDict.cancelable : false;

        return event;
      };
      if (existingProto) {
        Object.defineProperty(window.Event, 'prototype', {
          configurable: false,
          enumerable: false,
          writable: true,
          value: existingProto
        });
      }

      if (!('createEvent' in document)) {
        window.addEventListener = Window.prototype.addEventListener = Document.prototype.addEventListener = Element.prototype.addEventListener = function addEventListener() {
          var element = this,
              type = arguments[0],
              listener = arguments[1];

          if (element === window && type in unlistenableWindowEvents) {
            throw new Error('In IE8 the event: ' + type + ' is not available on the window object. Please see https://github.com/Financial-Times/polyfill-service/issues/317 for more information.');
          }

          if (!element._events) {
            element._events = {};
          }

          if (!element._events[type]) {
            element._events[type] = function (event) {
              var list = element._events[event.type].list,
                  events = list.slice(),
                  index = -1,
                  length = events.length,
                  eventElement;

              event.preventDefault = function preventDefault() {
                if (event.cancelable !== false) {
                  event.returnValue = false;
                }
              };

              event.stopPropagation = function stopPropagation() {
                event.cancelBubble = true;
              };

              event.stopImmediatePropagation = function stopImmediatePropagation() {
                event.cancelBubble = true;
                event.cancelImmediate = true;
              };

              event.currentTarget = element;
              event.relatedTarget = event.fromElement || null;
              event.target = event.target || event.srcElement || element;
              event.timeStamp = new Date().getTime();

              if (event.clientX) {
                event.pageX = event.clientX + document.documentElement.scrollLeft;
                event.pageY = event.clientY + document.documentElement.scrollTop;
              }

              while (++index < length && !event.cancelImmediate) {
                if (index in events) {
                  eventElement = events[index];

                  if (indexOf(list, eventElement) !== -1 && typeof eventElement === 'function') {
                    eventElement.call(element, event);
                  }
                }
              }
            };

            element._events[type].list = [];

            if (element.attachEvent) {
              element.attachEvent('on' + type, element._events[type]);
            }
          }

          element._events[type].list.push(listener);
        };

        window.removeEventListener = Window.prototype.removeEventListener = Document.prototype.removeEventListener = Element.prototype.removeEventListener = function removeEventListener() {
          var element = this,
              type = arguments[0],
              listener = arguments[1],
              index;

          if (element._events && element._events[type] && element._events[type].list) {
            index = indexOf(element._events[type].list, listener);

            if (index !== -1) {
              element._events[type].list.splice(index, 1);

              if (!element._events[type].list.length) {
                if (element.detachEvent) {
                  element.detachEvent('on' + type, element._events[type]);
                }
                delete element._events[type];
              }
            }
          }
        };

        window.dispatchEvent = Window.prototype.dispatchEvent = Document.prototype.dispatchEvent = Element.prototype.dispatchEvent = function dispatchEvent(event) {
          if (!arguments.length) {
            throw new Error('Not enough arguments');
          }

          if (!event || typeof event.type !== 'string') {
            throw new Error('DOM Events Exception 0');
          }

          var element = this,
              type = event.type;

          try {
            if (!event.bubbles) {
              event.cancelBubble = true;

              var cancelBubbleEvent = function cancelBubbleEvent(event) {
                event.cancelBubble = true;

                (element || window).detachEvent('on' + type, cancelBubbleEvent);
              };

              this.attachEvent('on' + type, cancelBubbleEvent);
            }

            this.fireEvent('on' + type, event);
          } catch (error) {
            event.target = element;

            do {
              event.currentTarget = element;

              if ('_events' in element && typeof element._events[type] === 'function') {
                element._events[type].call(element, event);
              }

              if (typeof element['on' + type] === 'function') {
                element['on' + type].call(element, event);
              }

              element = element.nodeType === 9 ? element.parentWindow : element.parentNode;
            } while (element && !event.cancelBubble);
          }

          return true;
        };

        // Add the DOMContentLoaded Event
        document.attachEvent('onreadystatechange', function () {
          if (document.readyState === 'complete') {
            document.dispatchEvent(new Event('DOMContentLoaded', {
              bubbles: true
            }));
          }
        });
      }
    })();
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  var KEY_SPACE = 32;
  var DEBOUNCE_TIMEOUT_IN_SECONDS = 1;

  function Button($module) {
    this.$module = $module;
    this.debounceFormSubmitTimer = null;
  }

  /**
  * JavaScript 'shim' to trigger the click event of element(s) when the space key is pressed.
  *
  * Created since some Assistive Technologies (for example some Screenreaders)
  * will tell a user to press space on a 'button', so this functionality needs to be shimmed
  * See https://github.com/alphagov/govuk_elements/pull/272#issuecomment-233028270
  *
  * @param {object} event event
  */
  Button.prototype.handleKeyDown = function (event) {
    // get the target element
    var target = event.target;
    // if the element has a role='button' and the pressed key is a space, we'll simulate a click
    if (target.getAttribute('role') === 'button' && event.keyCode === KEY_SPACE) {
      event.preventDefault();
      // trigger the target's click event
      target.click();
    }
  };

  /**
  * If the click quickly succeeds a previous click then nothing will happen.
  * This stops people accidentally causing multiple form submissions by
  * double clicking buttons.
  */
  Button.prototype.debounce = function (event) {
    var target = event.target;
    // Check the button that is clicked on has the preventDoubleClick feature enabled
    if (target.getAttribute('data-prevent-double-click') !== 'true') {
      return;
    }

    // If the timer is still running then we want to prevent the click from submitting the form
    if (this.debounceFormSubmitTimer) {
      event.preventDefault();
      return false;
    }

    this.debounceFormSubmitTimer = setTimeout((function () {
      this.debounceFormSubmitTimer = null;
    }).bind(this), DEBOUNCE_TIMEOUT_IN_SECONDS * 1000);
  };

  /**
  * Initialise an event listener for keydown at document level
  * this will help listening for later inserted elements with a role="button"
  */
  Button.prototype.init = function () {
    this.$module.addEventListener('keydown', this.handleKeyDown);
    this.$module.addEventListener('click', this.debounce);
  };

  /**
   * JavaScript 'polyfill' for HTML5's <details> and <summary> elements
   * and 'shim' to add accessiblity enhancements for all browsers
   *
   * http://caniuse.com/#feat=details
   */

  var KEY_ENTER = 13;
  var KEY_SPACE$1 = 32;

  function Details($module) {
    this.$module = $module;
  }

  Details.prototype.init = function () {
    if (!this.$module) {
      return;
    }

    // If there is native details support, we want to avoid running code to polyfill native behaviour.
    var hasNativeDetails = typeof this.$module.open === 'boolean';

    if (hasNativeDetails) {
      return;
    }

    this.polyfillDetails();
  };

  Details.prototype.polyfillDetails = function () {
    var $module = this.$module;

    // Save shortcuts to the inner summary and content elements
    var $summary = this.$summary = $module.getElementsByTagName('summary').item(0);
    var $content = this.$content = $module.getElementsByTagName('div').item(0);

    // If <details> doesn't have a <summary> and a <div> representing the content
    // it means the required HTML structure is not met so the script will stop
    if (!$summary || !$content) {
      return;
    }

    // If the content doesn't have an ID, assign it one now
    // which we'll need for the summary's aria-controls assignment
    if (!$content.id) {
      $content.id = 'details-content-' + generateUniqueID();
    }

    // Add ARIA role="group" to details
    $module.setAttribute('role', 'group');

    // Add role=button to summary
    $summary.setAttribute('role', 'button');

    // Add aria-controls
    $summary.setAttribute('aria-controls', $content.id);

    // Set tabIndex so the summary is keyboard accessible for non-native elements
    //
    // We have to use the camelcase `tabIndex` property as there is a bug in IE6/IE7 when we set the correct attribute lowercase:
    // See http://web.archive.org/web/20170120194036/http://www.saliences.com/browserBugs/tabIndex.html for more information.
    $summary.tabIndex = 0;

    // Detect initial open state
    var openAttr = $module.getAttribute('open') !== null;
    if (openAttr === true) {
      $summary.setAttribute('aria-expanded', 'true');
      $content.setAttribute('aria-hidden', 'false');
    } else {
      $summary.setAttribute('aria-expanded', 'false');
      $content.setAttribute('aria-hidden', 'true');
      $content.style.display = 'none';
    }

    // Bind an event to handle summary elements
    this.polyfillHandleInputs($summary, this.polyfillSetAttributes.bind(this));
  };

  /**
  * Define a statechange function that updates aria-expanded and style.display
  * @param {object} summary element
  */
  Details.prototype.polyfillSetAttributes = function () {
    var $module = this.$module;
    var $summary = this.$summary;
    var $content = this.$content;

    var expanded = $summary.getAttribute('aria-expanded') === 'true';
    var hidden = $content.getAttribute('aria-hidden') === 'true';

    $summary.setAttribute('aria-expanded', expanded ? 'false' : 'true');
    $content.setAttribute('aria-hidden', hidden ? 'false' : 'true');

    $content.style.display = expanded ? 'none' : '';

    var hasOpenAttr = $module.getAttribute('open') !== null;
    if (!hasOpenAttr) {
      $module.setAttribute('open', 'open');
    } else {
      $module.removeAttribute('open');
    }

    return true;
  };

  /**
  * Handle cross-modal click events
  * @param {object} node element
  * @param {function} callback function
  */
  Details.prototype.polyfillHandleInputs = function (node, callback) {
    node.addEventListener('keypress', function (event) {
      var target = event.target;
      // When the key gets pressed - check if it is enter or space
      if (event.keyCode === KEY_ENTER || event.keyCode === KEY_SPACE$1) {
        if (target.nodeName.toLowerCase() === 'summary') {
          // Prevent space from scrolling the page
          // and enter from submitting a form
          event.preventDefault();
          // Click to let the click event do all the necessary action
          if (target.click) {
            target.click();
          } else {
            // except Safari 5.1 and under don't support .click() here
            callback(event);
          }
        }
      }
    });

    // Prevent keyup to prevent clicking twice in Firefox when using space key
    node.addEventListener('keyup', function (event) {
      var target = event.target;
      if (event.keyCode === KEY_SPACE$1) {
        if (target.nodeName.toLowerCase() === 'summary') {
          event.preventDefault();
        }
      }
    });

    node.addEventListener('click', callback);
  };

  function CharacterCount($module) {
    this.$module = $module;
    this.$textarea = $module.querySelector('.govuk-js-character-count');
    if (this.$textarea) {
      this.$countMessage = $module.querySelector('[id=' + this.$textarea.id + '-info]');
    }
  }

  CharacterCount.prototype.defaults = {
    characterCountAttribute: 'data-maxlength',
    wordCountAttribute: 'data-maxwords'
  };

  // Initialize component
  CharacterCount.prototype.init = function () {
    // Check for module
    var $module = this.$module;
    var $textarea = this.$textarea;
    var $countMessage = this.$countMessage;

    if (!$textarea || !$countMessage) {
      return;
    }

    // We move count message right after the field
    // Kept for backwards compatibility
    $textarea.insertAdjacentElement('afterend', $countMessage);

    // Read options set using dataset ('data-' values)
    this.options = this.getDataset($module);

    // Determine the limit attribute (characters or words)
    var countAttribute = this.defaults.characterCountAttribute;
    if (this.options.maxwords) {
      countAttribute = this.defaults.wordCountAttribute;
    }

    // Save the element limit
    this.maxLength = $module.getAttribute(countAttribute);

    // Check for limit
    if (!this.maxLength) {
      return;
    }

    // Remove hard limit if set
    $module.removeAttribute('maxlength');

    // Bind event changes to the textarea
    var boundChangeEvents = this.bindChangeEvents.bind(this);
    boundChangeEvents();

    // Update count message
    var boundUpdateCountMessage = this.updateCountMessage.bind(this);
    boundUpdateCountMessage();
  };

  // Read data attributes
  CharacterCount.prototype.getDataset = function (element) {
    var dataset = {};
    var attributes = element.attributes;
    if (attributes) {
      for (var i = 0; i < attributes.length; i++) {
        var attribute = attributes[i];
        var match = attribute.name.match(/^data-(.+)/);
        if (match) {
          dataset[match[1]] = attribute.value;
        }
      }
    }
    return dataset;
  };

  // Counts characters or words in text
  CharacterCount.prototype.count = function (text) {
    var length;
    if (this.options.maxwords) {
      var tokens = text.match(/\S+/g) || []; // Matches consecutive non-whitespace chars
      length = tokens.length;
    } else {
      length = text.length;
    }
    return length;
  };

  // Bind input propertychange to the elements and update based on the change
  CharacterCount.prototype.bindChangeEvents = function () {
    var $textarea = this.$textarea;
    $textarea.addEventListener('keyup', this.checkIfValueChanged.bind(this));

    // Bind focus/blur events to start/stop polling
    $textarea.addEventListener('focus', this.handleFocus.bind(this));
    $textarea.addEventListener('blur', this.handleBlur.bind(this));
  };

  // Speech recognition software such as Dragon NaturallySpeaking will modify the
  // fields by directly changing its `value`. These changes don't trigger events
  // in JavaScript, so we need to poll to handle when and if they occur.
  CharacterCount.prototype.checkIfValueChanged = function () {
    if (!this.$textarea.oldValue) this.$textarea.oldValue = '';
    if (this.$textarea.value !== this.$textarea.oldValue) {
      this.$textarea.oldValue = this.$textarea.value;
      var boundUpdateCountMessage = this.updateCountMessage.bind(this);
      boundUpdateCountMessage();
    }
  };

  // Update message box
  CharacterCount.prototype.updateCountMessage = function () {
    var countElement = this.$textarea;
    var options = this.options;
    var countMessage = this.$countMessage;

    // Determine the remaining number of characters/words
    var currentLength = this.count(countElement.value);
    var maxLength = this.maxLength;
    var remainingNumber = maxLength - currentLength;

    // Set threshold if presented in options
    var thresholdPercent = options.threshold ? options.threshold : 0;
    var thresholdValue = maxLength * thresholdPercent / 100;
    if (thresholdValue > currentLength) {
      countMessage.classList.add('govuk-character-count__message--disabled');
      // Ensure threshold is hidden for users of assistive technologies
      countMessage.setAttribute('aria-hidden', true);
    } else {
      countMessage.classList.remove('govuk-character-count__message--disabled');
      // Ensure threshold is visible for users of assistive technologies
      countMessage.removeAttribute('aria-hidden');
    }

    // Update styles
    if (remainingNumber < 0) {
      countElement.classList.add('govuk-textarea--error');
      countMessage.classList.remove('govuk-hint');
      countMessage.classList.add('govuk-error-message');
    } else {
      countElement.classList.remove('govuk-textarea--error');
      countMessage.classList.remove('govuk-error-message');
      countMessage.classList.add('govuk-hint');
    }

    // Update message
    var charVerb = 'remaining';
    var charNoun = 'character';
    var displayNumber = remainingNumber;
    if (options.maxwords) {
      charNoun = 'word';
    }
    charNoun = charNoun + (remainingNumber === -1 || remainingNumber === 1 ? '' : 's');

    charVerb = remainingNumber < 0 ? 'too many' : 'remaining';
    displayNumber = Math.abs(remainingNumber);

    countMessage.innerHTML = 'You have ' + displayNumber + ' ' + charNoun + ' ' + charVerb;
  };

  CharacterCount.prototype.handleFocus = function () {
    // Check if value changed on focus
    this.valueChecker = setInterval(this.checkIfValueChanged.bind(this), 1000);
  };

  CharacterCount.prototype.handleBlur = function () {
    // Cancel value checking on blur
    clearInterval(this.valueChecker);
  };

  function Checkboxes($module) {
    this.$module = $module;
    this.$inputs = $module.querySelectorAll('input[type="checkbox"]');
  }

  Checkboxes.prototype.init = function () {
    var $module = this.$module;
    var $inputs = this.$inputs;

    /**
    * Loop over all items with [data-controls]
    * Check if they have a matching conditional reveal
    * If they do, assign attributes.
    **/
    nodeListForEach($inputs, (function ($input) {
      var controls = $input.getAttribute('data-aria-controls');

      // Check if input controls anything
      // Check if content exists, before setting attributes.
      if (!controls || !$module.querySelector('#' + controls)) {
        return;
      }

      // If we have content that is controlled, set attributes.
      $input.setAttribute('aria-controls', controls);
      $input.removeAttribute('data-aria-controls');
      this.setAttributes($input);
    }).bind(this));

    // Handle events
    $module.addEventListener('click', this.handleClick.bind(this));
  };

  Checkboxes.prototype.setAttributes = function ($input) {
    var inputIsChecked = $input.checked;
    $input.setAttribute('aria-expanded', inputIsChecked);

    var $content = this.$module.querySelector('#' + $input.getAttribute('aria-controls'));
    if ($content) {
      $content.classList.toggle('govuk-checkboxes__conditional--hidden', !inputIsChecked);
    }
  };

  Checkboxes.prototype.handleClick = function (event) {
    var $target = event.target;

    // If a checkbox with aria-controls, handle click
    var isCheckbox = $target.getAttribute('type') === 'checkbox';
    var hasAriaControls = $target.getAttribute('aria-controls');
    if (isCheckbox && hasAriaControls) {
      this.setAttributes($target);
    }
  };

  (function (undefined) {

    // Detection from https://raw.githubusercontent.com/Financial-Times/polyfill-service/1f3c09b402f65bf6e393f933a15ba63f1b86ef1f/packages/polyfill-library/polyfills/Element/prototype/matches/detect.js
    var detect = 'document' in this && "matches" in document.documentElement;

    if (detect) return;

    // Polyfill from https://raw.githubusercontent.com/Financial-Times/polyfill-service/1f3c09b402f65bf6e393f933a15ba63f1b86ef1f/packages/polyfill-library/polyfills/Element/prototype/matches/polyfill.js
    Element.prototype.matches = Element.prototype.webkitMatchesSelector || Element.prototype.oMatchesSelector || Element.prototype.msMatchesSelector || Element.prototype.mozMatchesSelector || function matches(selector) {
      var element = this;
      var elements = (element.document || element.ownerDocument).querySelectorAll(selector);
      var index = 0;

      while (elements[index] && elements[index] !== element) {
        ++index;
      }

      return !!elements[index];
    };
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {

    // Detection from https://raw.githubusercontent.com/Financial-Times/polyfill-service/1f3c09b402f65bf6e393f933a15ba63f1b86ef1f/packages/polyfill-library/polyfills/Element/prototype/closest/detect.js
    var detect = 'document' in this && "closest" in document.documentElement;

    if (detect) return;

    // Polyfill from https://raw.githubusercontent.com/Financial-Times/polyfill-service/1f3c09b402f65bf6e393f933a15ba63f1b86ef1f/packages/polyfill-library/polyfills/Element/prototype/closest/polyfill.js
    Element.prototype.closest = function closest(selector) {
      var node = this;

      while (node) {
        if (node.matches(selector)) return node;else node = 'SVGElement' in window && node instanceof SVGElement ? node.parentNode : node.parentElement;
      }

      return null;
    };
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  function ErrorSummary($module) {
    this.$module = $module;
  }

  ErrorSummary.prototype.init = function () {
    var $module = this.$module;
    if (!$module) {
      return;
    }
    $module.focus();

    $module.addEventListener('click', this.handleClick.bind(this));
  };

  /**
  * Click event handler
  *
  * @param {MouseEvent} event - Click event
  */
  ErrorSummary.prototype.handleClick = function (event) {
    var target = event.target;
    if (this.focusTarget(target)) {
      event.preventDefault();
    }
  };

  /**
   * Focus the target element
   *
   * By default, the browser will scroll the target into view. Because our labels
   * or legends appear above the input, this means the user will be presented with
   * an input without any context, as the label or legend will be off the top of
   * the screen.
   *
   * Manually handling the click event, scrolling the question into view and then
   * focussing the element solves this.
   *
   * This also results in the label and/or legend being announced correctly in
   * NVDA (as tested in 2018.3.2) - without this only the field type is announced
   * (e.g. "Edit, has autocomplete").
   *
   * @param {HTMLElement} $target - Event target
   * @returns {boolean} True if the target was able to be focussed
   */
  ErrorSummary.prototype.focusTarget = function ($target) {
    // If the element that was clicked was not a link, return early
    if ($target.tagName !== 'A' || $target.href === false) {
      return false;
    }

    var inputId = this.getFragmentFromUrl($target.href);
    var $input = document.getElementById(inputId);
    if (!$input) {
      return false;
    }

    var $legendOrLabel = this.getAssociatedLegendOrLabel($input);
    if (!$legendOrLabel) {
      return false;
    }

    // Scroll the legend or label into view *before* calling focus on the input to
    // avoid extra scrolling in browsers that don't support `preventScroll` (which
    // at time of writing is most of them...)
    $legendOrLabel.scrollIntoView();
    $input.focus({ preventScroll: true });

    return true;
  };

  /**
   * Get fragment from URL
   *
   * Extract the fragment (everything after the hash) from a URL, but not including
   * the hash.
   *
   * @param {string} url - URL
   * @returns {string} Fragment from URL, without the hash
   */
  ErrorSummary.prototype.getFragmentFromUrl = function (url) {
    if (url.indexOf('#') === -1) {
      return false;
    }

    return url.split('#').pop();
  };

  /**
   * Get associated legend or label
   *
   * Returns the first element that exists from this list:
   *
   * - The `<legend>` associated with the closest `<fieldset>` ancestor, as long
   *   as the top of it is no more than half a viewport height away from the
   *   bottom of the input
   * - The first `<label>` that is associated with the input using for="inputId"
   * - The closest parent `<label>`
   *
   * @param {HTMLElement} $input - The input
   * @returns {HTMLElement} Associated legend or label, or null if no associated
   *                        legend or label can be found
   */
  ErrorSummary.prototype.getAssociatedLegendOrLabel = function ($input) {
    var $fieldset = $input.closest('fieldset');

    if ($fieldset) {
      var legends = $fieldset.getElementsByTagName('legend');

      if (legends.length) {
        var $candidateLegend = legends[0];

        // If the input type is radio or checkbox, always use the legend if there
        // is one.
        if ($input.type === 'checkbox' || $input.type === 'radio') {
          return $candidateLegend;
        }

        // For other input types, only scroll to the fieldset’s legend (instead of
        // the label associated with the input) if the input would end up in the
        // top half of the screen.
        //
        // This should avoid situations where the input either ends up off the
        // screen, or obscured by a software keyboard.
        var legendTop = $candidateLegend.getBoundingClientRect().top;
        var inputRect = $input.getBoundingClientRect();

        // If the browser doesn't support Element.getBoundingClientRect().height
        // or window.innerHeight (like IE8), bail and just link to the label.
        if (inputRect.height && window.innerHeight) {
          var inputBottom = inputRect.top + inputRect.height;

          if (inputBottom - legendTop < window.innerHeight / 2) {
            return $candidateLegend;
          }
        }
      }
    }

    return document.querySelector("label[for='" + $input.getAttribute('id') + "']") || $input.closest('label');
  };

  function Header($module) {
    this.$module = $module;
  }

  Header.prototype.init = function () {
    // Check for module
    var $module = this.$module;
    if (!$module) {
      return;
    }

    // Check for button
    var $toggleButton = $module.querySelector('.govuk-js-header-toggle');
    if (!$toggleButton) {
      return;
    }

    // Handle $toggleButton click events
    $toggleButton.addEventListener('click', this.handleClick.bind(this));
  };

  /**
  * Toggle class
  * @param {object} node element
  * @param {string} className to toggle
  */
  Header.prototype.toggleClass = function (node, className) {
    if (node.className.indexOf(className) > 0) {
      node.className = node.className.replace(' ' + className, '');
    } else {
      node.className += ' ' + className;
    }
  };

  /**
  * An event handler for click event on $toggleButton
  * @param {object} event event
  */
  Header.prototype.handleClick = function (event) {
    var $module = this.$module;
    var $toggleButton = event.target || event.srcElement;
    var $target = $module.querySelector('#' + $toggleButton.getAttribute('aria-controls'));

    // If a button with aria-controls, handle click
    if ($toggleButton && $target) {
      this.toggleClass($target, 'govuk-header__navigation--open');
      this.toggleClass($toggleButton, 'govuk-header__menu-button--open');

      $toggleButton.setAttribute('aria-expanded', $toggleButton.getAttribute('aria-expanded') !== 'true');
      $target.setAttribute('aria-hidden', $target.getAttribute('aria-hidden') === 'false');
    }
  };

  function Radios($module) {
    this.$module = $module;
  }

  Radios.prototype.init = function () {
    var $module = this.$module;
    var $inputs = $module.querySelectorAll('input[type="radio"]');

    /**
    * Loop over all items with [data-controls]
    * Check if they have a matching conditional reveal
    * If they do, assign attributes.
    **/
    nodeListForEach($inputs, (function ($input) {
      var controls = $input.getAttribute('data-aria-controls');

      // Check if input controls anything
      // Check if content exists, before setting attributes.
      if (!controls || !$module.querySelector('#' + controls)) {
        return;
      }

      // If we have content that is controlled, set attributes.
      $input.setAttribute('aria-controls', controls);
      $input.removeAttribute('data-aria-controls');
      this.setAttributes($input);
    }).bind(this));

    // Handle events
    $module.addEventListener('click', this.handleClick.bind(this));
  };

  Radios.prototype.setAttributes = function ($input) {
    var $content = document.querySelector('#' + $input.getAttribute('aria-controls'));

    if ($content && $content.classList.contains('govuk-radios__conditional')) {
      var inputIsChecked = $input.checked;

      $input.setAttribute('aria-expanded', inputIsChecked);

      $content.classList.toggle('govuk-radios__conditional--hidden', !inputIsChecked);
    }
  };

  Radios.prototype.handleClick = function (event) {
    var $clickedInput = event.target;
    // We only want to handle clicks for radio inputs
    if ($clickedInput.type !== 'radio') {
      return;
    }
    // Because checking one radio can uncheck a radio in another $module,
    // we need to call set attributes on all radios in the same form, or document if they're not in a form.
    //
    // We also only want radios which have aria-controls, as they support conditional reveals.
    var $allInputs = document.querySelectorAll('input[type="radio"][aria-controls]');
    nodeListForEach($allInputs, (function ($input) {
      // Only inputs with the same form owner should change.
      var hasSameFormOwner = $input.form === $clickedInput.form;

      // In radios, only radios with the same name will affect each other.
      var hasSameName = $input.name === $clickedInput.name;
      if (hasSameName && hasSameFormOwner) {
        this.setAttributes($input);
      }
    }).bind(this));
  };

  (function (undefined) {

    // Detection from https://github.com/Financial-Times/polyfill-service/pull/1062/files#diff-b09a5d2acf3314b46a6c8f8d0c31b85c
    var detect = 'Element' in this && "nextElementSibling" in document.documentElement;

    if (detect) return;

    (function (global) {

      // Polyfill from https://github.com/Financial-Times/polyfill-service/pull/1062/files#diff-404b69b4750d18dea4174930a49170fd
      Object.defineProperty(Element.prototype, "nextElementSibling", {
        get: function get() {
          var el = this.nextSibling;
          while (el && el.nodeType !== 1) {
            el = el.nextSibling;
          }
          return el.nodeType === 1 ? el : null;
        }
      });
    })(this);
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  (function (undefined) {

    // Detection from https://github.com/Financial-Times/polyfill-service/pull/1062/files#diff-a162235fbc9c0dd40d4032265f44942e
    var detect = 'Element' in this && 'previousElementSibling' in document.documentElement;

    if (detect) return;

    (function (global) {
      // Polyfill from https://github.com/Financial-Times/polyfill-service/pull/1062/files#diff-b45a1197b842728cb76b624b6ba7d739
      Object.defineProperty(Element.prototype, 'previousElementSibling', {
        get: function get() {
          var el = this.previousSibling;
          while (el && el.nodeType !== 1) {
            el = el.previousSibling;
          }
          return el.nodeType === 1 ? el : null;
        }
      });
    })(this);
  }).call('object' === typeof window && window || 'object' === typeof self && self || 'object' === typeof global && global || {});

  function Tabs($module) {
    this.$module = $module;
    this.$tabs = $module.querySelectorAll('.govuk-tabs__tab');

    this.keys = { left: 37, right: 39, up: 38, down: 40 };
    this.jsHiddenClass = 'govuk-tabs__panel--hidden';
  }

  Tabs.prototype.init = function () {
    if (typeof window.matchMedia === 'function') {
      this.setupResponsiveChecks();
    } else {
      this.setup();
    }
  };

  Tabs.prototype.setupResponsiveChecks = function () {
    this.mql = window.matchMedia('(min-width: 40.0625em)');
    this.mql.addListener(this.checkMode.bind(this));
    this.checkMode();
  };

  Tabs.prototype.checkMode = function () {
    if (this.mql.matches) {
      this.setup();
    } else {
      this.teardown();
    }
  };

  Tabs.prototype.setup = function () {
    var $module = this.$module;
    var $tabs = this.$tabs;
    var $tabList = $module.querySelector('.govuk-tabs__list');
    var $tabListItems = $module.querySelectorAll('.govuk-tabs__list-item');

    if (!$tabs || !$tabList || !$tabListItems) {
      return;
    }

    $tabList.setAttribute('role', 'tablist');

    nodeListForEach($tabListItems, function ($item) {
      $item.setAttribute('role', 'presentation');
    });

    nodeListForEach($tabs, (function ($tab) {
      // Set HTML attributes
      this.setAttributes($tab);

      // Save bounded functions to use when removing event listeners during teardown
      $tab.boundTabClick = this.onTabClick.bind(this);
      $tab.boundTabKeydown = this.onTabKeydown.bind(this);

      // Handle events
      $tab.addEventListener('click', $tab.boundTabClick, true);
      $tab.addEventListener('keydown', $tab.boundTabKeydown, true);

      // Remove old active panels
      this.hideTab($tab);
    }).bind(this));

    // Show either the active tab according to the URL's hash or the first tab
    var $activeTab = this.getTab(window.location.hash) || this.$tabs[0];
    this.showTab($activeTab);

    // Handle hashchange events
    $module.boundOnHashChange = this.onHashChange.bind(this);
    window.addEventListener('hashchange', $module.boundOnHashChange, true);
  };

  Tabs.prototype.teardown = function () {
    var $module = this.$module;
    var $tabs = this.$tabs;
    var $tabList = $module.querySelector('.govuk-tabs__list');
    var $tabListItems = $module.querySelectorAll('.govuk-tabs__list-item');

    if (!$tabs || !$tabList || !$tabListItems) {
      return;
    }

    $tabList.removeAttribute('role');

    nodeListForEach($tabListItems, function ($item) {
      $item.removeAttribute('role', 'presentation');
    });

    nodeListForEach($tabs, (function ($tab) {
      // Remove events
      $tab.removeEventListener('click', $tab.boundTabClick, true);
      $tab.removeEventListener('keydown', $tab.boundTabKeydown, true);

      // Unset HTML attributes
      this.unsetAttributes($tab);
    }).bind(this));

    // Remove hashchange event handler
    window.removeEventListener('hashchange', $module.boundOnHashChange, true);
  };

  Tabs.prototype.onHashChange = function (e) {
    var hash = window.location.hash;
    var $tabWithHash = this.getTab(hash);
    if (!$tabWithHash) {
      return;
    }

    // Prevent changing the hash
    if (this.changingHash) {
      this.changingHash = false;
      return;
    }

    // Show either the active tab according to the URL's hash or the first tab
    var $previousTab = this.getCurrentTab();

    this.hideTab($previousTab);
    this.showTab($tabWithHash);
    $tabWithHash.focus();
  };

  Tabs.prototype.hideTab = function ($tab) {
    this.unhighlightTab($tab);
    this.hidePanel($tab);
  };

  Tabs.prototype.showTab = function ($tab) {
    this.highlightTab($tab);
    this.showPanel($tab);
  };

  Tabs.prototype.getTab = function (hash) {
    return this.$module.querySelector('.govuk-tabs__tab[href="' + hash + '"]');
  };

  Tabs.prototype.setAttributes = function ($tab) {
    // set tab attributes
    var panelId = this.getHref($tab).slice(1);
    $tab.setAttribute('id', 'tab_' + panelId);
    $tab.setAttribute('role', 'tab');
    $tab.setAttribute('aria-controls', panelId);
    $tab.setAttribute('aria-selected', 'false');
    $tab.setAttribute('tabindex', '-1');

    // set panel attributes
    var $panel = this.getPanel($tab);
    $panel.setAttribute('role', 'tabpanel');
    $panel.setAttribute('aria-labelledby', $tab.id);
    $panel.classList.add(this.jsHiddenClass);
  };

  Tabs.prototype.unsetAttributes = function ($tab) {
    // unset tab attributes
    $tab.removeAttribute('id');
    $tab.removeAttribute('role');
    $tab.removeAttribute('aria-controls');
    $tab.removeAttribute('aria-selected');
    $tab.removeAttribute('tabindex');

    // unset panel attributes
    var $panel = this.getPanel($tab);
    $panel.removeAttribute('role');
    $panel.removeAttribute('aria-labelledby');
    $panel.classList.remove(this.jsHiddenClass);
  };

  Tabs.prototype.onTabClick = function (e) {
    if (!e.target.classList.contains('govuk-tabs__tab')) {
      // Allow events on child DOM elements to bubble up to tab parent
      return false;
    }
    e.preventDefault();
    var $newTab = e.target;
    var $currentTab = this.getCurrentTab();
    this.hideTab($currentTab);
    this.showTab($newTab);
    this.createHistoryEntry($newTab);
  };

  Tabs.prototype.createHistoryEntry = function ($tab) {
    var $panel = this.getPanel($tab);

    // Save and restore the id
    // so the page doesn't jump when a user clicks a tab (which changes the hash)
    var id = $panel.id;
    $panel.id = '';
    this.changingHash = true;
    window.location.hash = this.getHref($tab).slice(1);
    $panel.id = id;
  };

  Tabs.prototype.onTabKeydown = function (e) {
    switch (e.keyCode) {
      case this.keys.left:
      case this.keys.up:
        this.activatePreviousTab();
        e.preventDefault();
        break;
      case this.keys.right:
      case this.keys.down:
        this.activateNextTab();
        e.preventDefault();
        break;
    }
  };

  Tabs.prototype.activateNextTab = function () {
    var currentTab = this.getCurrentTab();
    var nextTabListItem = currentTab.parentNode.nextElementSibling;
    if (nextTabListItem) {
      var nextTab = nextTabListItem.querySelector('.govuk-tabs__tab');
    }
    if (nextTab) {
      this.hideTab(currentTab);
      this.showTab(nextTab);
      nextTab.focus();
      this.createHistoryEntry(nextTab);
    }
  };

  Tabs.prototype.activatePreviousTab = function () {
    var currentTab = this.getCurrentTab();
    var previousTabListItem = currentTab.parentNode.previousElementSibling;
    if (previousTabListItem) {
      var previousTab = previousTabListItem.querySelector('.govuk-tabs__tab');
    }
    if (previousTab) {
      this.hideTab(currentTab);
      this.showTab(previousTab);
      previousTab.focus();
      this.createHistoryEntry(previousTab);
    }
  };

  Tabs.prototype.getPanel = function ($tab) {
    var $panel = this.$module.querySelector(this.getHref($tab));
    return $panel;
  };

  Tabs.prototype.showPanel = function ($tab) {
    var $panel = this.getPanel($tab);
    $panel.classList.remove(this.jsHiddenClass);
  };

  Tabs.prototype.hidePanel = function (tab) {
    var $panel = this.getPanel(tab);
    $panel.classList.add(this.jsHiddenClass);
  };

  Tabs.prototype.unhighlightTab = function ($tab) {
    $tab.setAttribute('aria-selected', 'false');
    $tab.parentNode.classList.remove('govuk-tabs__list-item--selected');
    $tab.setAttribute('tabindex', '-1');
  };

  Tabs.prototype.highlightTab = function ($tab) {
    $tab.setAttribute('aria-selected', 'true');
    $tab.parentNode.classList.add('govuk-tabs__list-item--selected');
    $tab.setAttribute('tabindex', '0');
  };

  Tabs.prototype.getCurrentTab = function () {
    return this.$module.querySelector('.govuk-tabs__list-item--selected .govuk-tabs__tab');
  };

  // this is because IE doesn't always return the actual value but a relative full path
  // should be a utility function most prob
  // http://labs.thesedays.com/blog/2010/01/08/getting-the-href-value-with-jquery-in-ie/
  Tabs.prototype.getHref = function ($tab) {
    var href = $tab.getAttribute('href');
    var hash = href.slice(href.indexOf('#'), href.length);
    return hash;
  };

  function initAll(options) {
    // Set the options to an empty object by default if no options are passed.
    options = typeof options !== 'undefined' ? options : {};

    // Allow the user to initialise GOV.UK Frontend in only certain sections of the page
    // Defaults to the entire document if nothing is set.
    var scope = typeof options.scope !== 'undefined' ? options.scope : document;

    var $buttons = scope.querySelectorAll('[data-module="govuk-button"]');
    nodeListForEach($buttons, function ($button) {
      new Button($button).init();
    });

    var $accordions = scope.querySelectorAll('[data-module="govuk-accordion"]');
    nodeListForEach($accordions, function ($accordion) {
      new Accordion($accordion).init();
    });

    var $details = scope.querySelectorAll('[data-module="govuk-details"]');
    nodeListForEach($details, function ($detail) {
      new Details($detail).init();
    });

    var $characterCounts = scope.querySelectorAll('[data-module="govuk-character-count"]');
    nodeListForEach($characterCounts, function ($characterCount) {
      new CharacterCount($characterCount).init();
    });

    var $checkboxes = scope.querySelectorAll('[data-module="govuk-checkboxes"]');
    nodeListForEach($checkboxes, function ($checkbox) {
      new Checkboxes($checkbox).init();
    });

    // Find first error summary module to enhance.
    var $errorSummary = scope.querySelector('[data-module="govuk-error-summary"]');
    new ErrorSummary($errorSummary).init();

    // Find first header module to enhance.
    var $toggleButton = scope.querySelector('[data-module="govuk-header"]');
    new Header($toggleButton).init();

    var $radios = scope.querySelectorAll('[data-module="govuk-radios"]');
    nodeListForEach($radios, function ($radio) {
      new Radios($radio).init();
    });

    var $tabs = scope.querySelectorAll('[data-module="govuk-tabs"]');
    nodeListForEach($tabs, function ($tabs) {
      new Tabs($tabs).init();
    });
  }

  exports.initAll = initAll;
  exports.Accordion = Accordion;
  exports.Button = Button;
  exports.Details = Details;
  exports.CharacterCount = CharacterCount;
  exports.Checkboxes = Checkboxes;
  exports.ErrorSummary = ErrorSummary;
  exports.Header = Header;
  exports.Radios = Radios;
  exports.Tabs = Tabs;
});

//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIkM6L0dpdFJlcG9zL0RDLUxBUlMvc3JjL0VTRkEuREMuTEFSUy9FU0ZBLkRDLkxBUlMuV2ViL25vZGVfbW9kdWxlcy9nb3Z1ay1mcm9udGVuZC9nb3Z1ay9hbGwuanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7QUFBQSxBQUFDLENBQUEsVUFBVSxNQUFNLEVBQUUsT0FBTyxFQUFFO0FBQzNCLFNBQU8sT0FBTyxLQUFLLFFBQVEsSUFBSSxPQUFPLE1BQU0sS0FBSyxXQUFXLEdBQUcsT0FBTyxDQUFDLE9BQU8sQ0FBQyxHQUMvRSxPQUFPLE1BQU0sS0FBSyxVQUFVLElBQUksTUFBTSxDQUFDLEdBQUcsR0FBRyxNQUFNLENBQUMsZUFBZSxFQUFFLENBQUMsU0FBUyxDQUFDLEVBQUUsT0FBTyxDQUFDLEdBQ3pGLE9BQU8sQ0FBRSxNQUFNLENBQUMsYUFBYSxHQUFHLEVBQUUsQ0FBRSxBQUFDLENBQUM7Q0FDdkMsQ0FBQSxZQUFRLFVBQVUsT0FBTyxFQUFFO0FBQUUsY0FBWSxDQUFDOzs7Ozs7O0FBTzNDLFdBQVMsZUFBZSxDQUFFLEtBQUssRUFBRSxRQUFRLEVBQUU7QUFDekMsUUFBSSxNQUFNLENBQUMsUUFBUSxDQUFDLFNBQVMsQ0FBQyxPQUFPLEVBQUU7QUFDckMsYUFBTyxLQUFLLENBQUMsT0FBTyxDQUFDLFFBQVEsQ0FBQyxDQUFBO0tBQy9CO0FBQ0QsU0FBSyxJQUFJLENBQUMsR0FBRyxDQUFDLEVBQUUsQ0FBQyxHQUFHLEtBQUssQ0FBQyxNQUFNLEVBQUUsQ0FBQyxFQUFFLEVBQUU7QUFDckMsY0FBUSxDQUFDLElBQUksQ0FBQyxNQUFNLEVBQUUsS0FBSyxDQUFDLENBQUMsQ0FBQyxFQUFFLENBQUMsRUFBRSxLQUFLLENBQUMsQ0FBQztLQUMzQztHQUNGOzs7OztBQUtELFdBQVMsZ0JBQWdCLEdBQUk7QUFDM0IsUUFBSSxDQUFDLEdBQUcsSUFBSSxJQUFJLEVBQUUsQ0FBQyxPQUFPLEVBQUUsQ0FBQztBQUM3QixRQUFJLE9BQU8sTUFBTSxDQUFD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RyxZQUFZLENBQUM7QUFDcEMsU0FBTyxDQUFDLE1BQU0sR0FBRyxNQUFNLENBQUM7QUFDeEIsU0FBTyxDQUFDLE1BQU0sR0FBRyxNQUFNLENBQUM7QUFDeEIsU0FBTyxDQUFDLElBQUksR0FBRyxJQUFJLENBQUM7Q0FFbkIsQ0FBRSxDQUFFIiwiZmlsZSI6InN0ZG91dCIsInNvdXJjZXNDb250ZW50IjpbIihmdW5jdGlvbiAoZ2xvYmFsLCBmYWN0b3J5KSB7XG5cdHR5cGVvZiBleHBvcnRzID09PSAnb2JqZWN0JyAmJiB0eXBlb2YgbW9kdWxlICE9PSAndW5kZWZpbmVkJyA/IGZhY3RvcnkoZXhwb3J0cykgOlxuXHR0eXBlb2YgZGVmaW5lID09PSAnZnVuY3Rpb24nICYmIGRlZmluZS5hbWQgPyBkZWZpbmUoJ0dPVlVLRnJvbnRlbmQnLCBbJ2V4cG9ydHMnXSwgZmFjdG9yeSkgOlxuXHQoZmFjdG9yeSgoZ2xvYmFsLkdPVlVLRnJvbnRlbmQgPSB7fSkpKTtcbn0odGhpcywgKGZ1bmN0aW9uIChleHBvcnRzKSB7ICd1c2Ugc3RyaWN0JztcblxuLyoqXG4gKiBUT0RPOiBJZGVhbGx5IHRoaXMgd291bGQgYmUgYSBOb2RlTGlzdC5wcm90b3R5cGUuZm9yRWFjaCBwb2x5ZmlsbFxuICogVGhpcyBzZWVtcyB0byBmYWlsIGluIElFOCwgcmVxdWlyZXMgbW9yZSBpbnZlc3RpZ2F0aW9uLlxuICogU2VlOiBodHRwczovL2dpdGh1Yi5jb20vaW1hZ2l0YW1hL25vZGVsaXN0LWZvcmVhY2gtcG9seWZpbGxcbiAqL1xuZnVuY3Rpb24gbm9kZUxpc3RGb3JFYWNoIChub2RlcywgY2FsbGJhY2spIHtcbiAgaWYgKHdpbmRvdy5Ob2RlTGlzdC5wcm90b3R5cGUuZm9yRWFjaCkge1xuICAgIHJldHVybiBub2Rlcy5mb3JFYWNoKGNhbGxiYWNrKVxuICB9XG4gIGZvciAodmFyIGkgPSAwOyBpIDwgbm9kZXMubGVuZ3RoOyBpKyspIHtcbiAgICBjYWxsYmFjay5jYWxsKHdpbmRvdywgbm9kZXNbaV0sIGksIG5vZGVzKTtcbiAgfVxufVxuXG4vLyBVc2VkIHRvIGdlbmVyYXRlIGEgdW5pcXVlIHN0cmluZywgYWxsb3dzIG11bHRpcGxlIGluc3RhbmNlcyBvZiB0aGUgY29tcG9uZW50IHdpdGhvdXRcbi8vIFRoZW0gY29uZmxpY3Rpbmcgd2l0aCBlYWNoIG90aGVyLlxuLy8gaHR0cHM6Ly9zdGFja292ZXJmbG93LmNvbS9hLzg4MDk0NzJcbmZ1bmN0aW9uIGdlbmVyYXRlVW5pcXVlSUQgKCkge1xuICB2YXIgZCA9IG5ldyBEYXRlKCkuZ2V0VGltZSgpO1xuICBpZiAodHlwZW9mIHdpbmRvdy5wZXJmb3JtYW5jZSAhPT0gJ3VuZGVmaW5lZCcgJiYgdHlwZW9mIHdpbmRvdy5wZXJmb3JtYW5jZS5ub3cgPT09ICdmdW5jdGlvbicpIHtcbiAgICBkICs9IHdpbmRvdy5wZXJmb3JtYW5jZS5ub3coKTsgLy8gdXNlIGhpZ2gtcHJlY2lzaW9uIHRpbWVyIGlmIGF2YWlsYWJsZVxuICB9XG4gIHJldHVybiAneHh4eHh4eHgteHh4eC00eHh4LXl4eHgteHh4eHh4eHh4eHh4Jy5yZXBsYWNlKC9beHldL2csIGZ1bmN0aW9uIChjKSB7XG4gICAgdmFyIHIgPSAoZCArIE1hdGgucmFuZG9tKCkgKiAxNikgJSAxNiB8IDA7XG4gICAgZCA9IE1hdGguZmxvb3IoZCAvIDE2KTtcbiAgICByZXR1cm4gKGMgPT09ICd4JyA/IHIgOiAociAmIDB4MyB8IDB4OCkpLnRvU3RyaW5nKDE2KVxuICB9KVxufVxuXG4oZnVuY3Rpb24odW5kZWZpbmVkKSB7XG5cbi8vIERldGVjdGlvbiBmcm9tIGh0dHBzOi8vZ2l0aHViLmNvbS9GaW5hbmNpYWwtVGltZXMvcG9seWZpbGwtc2VydmljZS9ibG9iL21hc3Rlci9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9PYmplY3QvZGVmaW5lUHJvcGVydHkvZGV0ZWN0LmpzXG52YXIgZGV0ZWN0ID0gKFxuICAvLyBJbiBJRTgsIGRlZmluZVByb3BlcnR5IGNvdWxkIG9ubHkgYWN0IG9uIERPTSBlbGVtZW50cywgc28gZnVsbCBzdXBwb3J0XG4gIC8vIGZvciB0aGUgZmVhdHVyZSByZXF1aXJlcyB0aGUgYWJpbGl0eSB0byBzZXQgYSBwcm9wZXJ0eSBvbiBhbiBhcmJpdHJhcnkgb2JqZWN0XG4gICdkZWZpbmVQcm9wZXJ0eScgaW4gT2JqZWN0ICYmIChmdW5jdGlvbigpIHtcbiAgXHR0cnkge1xuICBcdFx0dmFyIGEgPSB7fTtcbiAgXHRcdE9iamVjdC5kZWZpbmVQcm9wZXJ0eShhLCAndGVzdCcsIHt2YWx1ZTo0Mn0pO1xuICBcdFx0cmV0dXJuIHRydWU7XG4gIFx0fSBjYXRjaChlKSB7XG4gIFx0XHRyZXR1cm4gZmFsc2VcbiAgXHR9XG4gIH0oKSlcbik7XG5cbmlmIChkZXRlY3QpIHJldHVyblxuXG4vLyBQb2x5ZmlsbCBmcm9tIGh0dHBzOi8vY2RuLnBvbHlmaWxsLmlvL3YyL3BvbHlmaWxsLmpzP2ZlYXR1cmVzPU9iamVjdC5kZWZpbmVQcm9wZXJ0eSZmbGFncz1hbHdheXNcbihmdW5jdGlvbiAobmF0aXZlRGVmaW5lUHJvcGVydHkpIHtcblxuXHR2YXIgc3VwcG9ydHNBY2Nlc3NvcnMgPSBPYmplY3QucHJvdG90eXBlLmhhc093blByb3BlcnR5KCdfX2RlZmluZUdldHRlcl9fJyk7XG5cdHZhciBFUlJfQUNDRVNTT1JTX05PVF9TVVBQT1JURUQgPSAnR2V0dGVycyAmIHNldHRlcnMgY2Fubm90IGJlIGRlZmluZWQgb24gdGhpcyBqYXZhc2NyaXB0IGVuZ2luZSc7XG5cdHZhciBFUlJfVkFMVUVfQUNDRVNTT1JTID0gJ0EgcHJvcGVydHkgY2Fubm90IGJvdGggaGF2ZSBhY2Nlc3NvcnMgYW5kIGJlIHdyaXRhYmxlIG9yIGhhdmUgYSB2YWx1ZSc7XG5cblx0T2JqZWN0LmRlZmluZVByb3BlcnR5ID0gZnVuY3Rpb24gZGVmaW5lUHJvcGVydHkob2JqZWN0LCBwcm9wZXJ0eSwgZGVzY3JpcHRvcikge1xuXG5cdFx0Ly8gV2hlcmUgbmF0aXZlIHN1cHBvcnQgZXhpc3RzLCBhc3N1bWUgaXRcblx0XHRpZiAobmF0aXZlRGVmaW5lUHJvcGVydHkgJiYgKG9iamVjdCA9PT0gd2luZG93IHx8IG9iamVjdCA9PT0gZG9jdW1lbnQgfHwgb2JqZWN0ID09PSBFbGVtZW50LnByb3RvdHlwZSB8fCBvYmplY3QgaW5zdGFuY2VvZiBFbGVtZW50KSkge1xuXHRcdFx0cmV0dXJuIG5hdGl2ZURlZmluZVByb3BlcnR5KG9iamVjdCwgcHJvcGVydHksIGRlc2NyaXB0b3IpO1xuXHRcdH1cblxuXHRcdGlmIChvYmplY3QgPT09IG51bGwgfHwgIShvYmplY3QgaW5zdGFuY2VvZiBPYmplY3QgfHwgdHlwZW9mIG9iamVjdCA9PT0gJ29iamVjdCcpKSB7XG5cdFx0XHR0aHJvdyBuZXcgVHlwZUVycm9yKCdPYmplY3QuZGVmaW5lUHJvcGVydHkgY2FsbGVkIG9uIG5vbi1vYmplY3QnKTtcblx0XHR9XG5cblx0XHRpZiAoIShkZXNjcmlwdG9yIGluc3RhbmNlb2YgT2JqZWN0KSkge1xuXHRcdFx0dGhyb3cgbmV3IFR5cGVFcnJvcignUHJvcGVydHkgZGVzY3JpcHRpb24gbXVzdCBiZSBhbiBvYmplY3QnKTtcblx0XHR9XG5cblx0XHR2YXIgcHJvcGVydHlTdHJpbmcgPSBTdHJpbmcocHJvcGVydHkpO1xuXHRcdHZhciBoYXNWYWx1ZU9yV3JpdGFibGUgPSAndmFsdWUnIGluIGRlc2NyaXB0b3IgfHwgJ3dyaXRhYmxlJyBpbiBkZXNjcmlwdG9yO1xuXHRcdHZhciBnZXR0ZXJUeXBlID0gJ2dldCcgaW4gZGVzY3JpcHRvciAmJiB0eXBlb2YgZGVzY3JpcHRvci5nZXQ7XG5cdFx0dmFyIHNldHRlclR5cGUgPSAnc2V0JyBpbiBkZXNjcmlwdG9yICYmIHR5cGVvZiBkZXNjcmlwdG9yLnNldDtcblxuXHRcdC8vIGhhbmRsZSBkZXNjcmlwdG9yLmdldFxuXHRcdGlmIChnZXR0ZXJUeXBlKSB7XG5cdFx0XHRpZiAoZ2V0dGVyVHlwZSAhPT0gJ2Z1bmN0aW9uJykge1xuXHRcdFx0XHR0aHJvdyBuZXcgVHlwZUVycm9yKCdHZXR0ZXIgbXVzdCBiZSBhIGZ1bmN0aW9uJyk7XG5cdFx0XHR9XG5cdFx0XHRpZiAoIXN1cHBvcnRzQWNjZXNzb3JzKSB7XG5cdFx0XHRcdHRocm93IG5ldyBUeXBlRXJyb3IoRVJSX0FDQ0VTU09SU19OT1RfU1VQUE9SVEVEKTtcblx0XHRcdH1cblx0XHRcdGlmIChoYXNWYWx1ZU9yV3JpdGFibGUpIHtcblx0XHRcdFx0dGhyb3cgbmV3IFR5cGVFcnJvcihFUlJfVkFMVUVfQUNDRVNTT1JTKTtcblx0XHRcdH1cblx0XHRcdE9iamVjdC5fX2RlZmluZUdldHRlcl9fLmNhbGwob2JqZWN0LCBwcm9wZXJ0eVN0cmluZywgZGVzY3JpcHRvci5nZXQpO1xuXHRcdH0gZWxzZSB7XG5cdFx0XHRvYmplY3RbcHJvcGVydHlTdHJpbmddID0gZGVzY3JpcHRvci52YWx1ZTtcblx0XHR9XG5cblx0XHQvLyBoYW5kbGUgZGVzY3JpcHRvci5zZXRcblx0XHRpZiAoc2V0dGVyVHlwZSkge1xuXHRcdFx0aWYgKHNldHRlclR5cGUgIT09ICdmdW5jdGlvbicpIHtcblx0XHRcdFx0dGhyb3cgbmV3IFR5cGVFcnJvcignU2V0dGVyIG11c3QgYmUgYSBmdW5jdGlvbicpO1xuXHRcdFx0fVxuXHRcdFx0aWYgKCFzdXBwb3J0c0FjY2Vzc29ycykge1xuXHRcdFx0XHR0aHJvdyBuZXcgVHlwZUVycm9yKEVSUl9BQ0NFU1NPUlNfTk9UX1NVUFBPUlRFRCk7XG5cdFx0XHR9XG5cdFx0XHRpZiAoaGFzVmFsdWVPcldyaXRhYmxlKSB7XG5cdFx0XHRcdHRocm93IG5ldyBUeXBlRXJyb3IoRVJSX1ZBTFVFX0FDQ0VTU09SUyk7XG5cdFx0XHR9XG5cdFx0XHRPYmplY3QuX19kZWZpbmVTZXR0ZXJfXy5jYWxsKG9iamVjdCwgcHJvcGVydHlTdHJpbmcsIGRlc2NyaXB0b3Iuc2V0KTtcblx0XHR9XG5cblx0XHQvLyBPSyB0byBkZWZpbmUgdmFsdWUgdW5jb25kaXRpb25hbGx5IC0gaWYgYSBnZXR0ZXIgaGFzIGJlZW4gc3BlY2lmaWVkIGFzIHdlbGwsIGFuIGVycm9yIHdvdWxkIGJlIHRocm93biBhYm92ZVxuXHRcdGlmICgndmFsdWUnIGluIGRlc2NyaXB0b3IpIHtcblx0XHRcdG9iamVjdFtwcm9wZXJ0eVN0cmluZ10gPSBkZXNjcmlwdG9yLnZhbHVlO1xuXHRcdH1cblxuXHRcdHJldHVybiBvYmplY3Q7XG5cdH07XG59KE9iamVjdC5kZWZpbmVQcm9wZXJ0eSkpO1xufSlcbi5jYWxsKCdvYmplY3QnID09PSB0eXBlb2Ygd2luZG93ICYmIHdpbmRvdyB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIHNlbGYgJiYgc2VsZiB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIGdsb2JhbCAmJiBnbG9iYWwgfHwge30pO1xuXG4oZnVuY3Rpb24odW5kZWZpbmVkKSB7XG4gIC8vIERldGVjdGlvbiBmcm9tIGh0dHBzOi8vZ2l0aHViLmNvbS9GaW5hbmNpYWwtVGltZXMvcG9seWZpbGwtc2VydmljZS9ibG9iL21hc3Rlci9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9GdW5jdGlvbi9wcm90b3R5cGUvYmluZC9kZXRlY3QuanNcbiAgdmFyIGRldGVjdCA9ICdiaW5kJyBpbiBGdW5jdGlvbi5wcm90b3R5cGU7XG5cbiAgaWYgKGRldGVjdCkgcmV0dXJuXG5cbiAgLy8gUG9seWZpbGwgZnJvbSBodHRwczovL2Nkbi5wb2x5ZmlsbC5pby92Mi9wb2x5ZmlsbC5qcz9mZWF0dXJlcz1GdW5jdGlvbi5wcm90b3R5cGUuYmluZCZmbGFncz1hbHdheXNcbiAgT2JqZWN0LmRlZmluZVByb3BlcnR5KEZ1bmN0aW9uLnByb3RvdHlwZSwgJ2JpbmQnLCB7XG4gICAgICB2YWx1ZTogZnVuY3Rpb24gYmluZCh0aGF0KSB7IC8vIC5sZW5ndGggaXMgMVxuICAgICAgICAgIC8vIGFkZCBuZWNlc3NhcnkgZXM1LXNoaW0gdXRpbGl0aWVzXG4gICAgICAgICAgdmFyICRBcnJheSA9IEFycmF5O1xuICAgICAgICAgIHZhciAkT2JqZWN0ID0gT2JqZWN0O1xuICAgICAgICAgIHZhciBPYmplY3RQcm90b3R5cGUgPSAkT2JqZWN0LnByb3RvdHlwZTtcbiAgICAgICAgICB2YXIgQXJyYXlQcm90b3R5cGUgPSAkQXJyYXkucHJvdG90eXBlO1xuICAgICAgICAgIHZhciBFbXB0eSA9IGZ1bmN0aW9uIEVtcHR5KCkge307XG4gICAgICAgICAgdmFyIHRvX3N0cmluZyA9IE9iamVjdFByb3RvdHlwZS50b1N0cmluZztcbiAgICAgICAgICB2YXIgaGFzVG9TdHJpbmdUYWcgPSB0eXBlb2YgU3ltYm9sID09PSAnZnVuY3Rpb24nICYmIHR5cGVvZiBTeW1ib2wudG9TdHJpbmdUYWcgPT09ICdzeW1ib2wnO1xuICAgICAgICAgIHZhciBpc0NhbGxhYmxlOyAvKiBpbmxpbmVkIGZyb20gaHR0cHM6Ly9ucG1qcy5jb20vaXMtY2FsbGFibGUgKi8gdmFyIGZuVG9TdHIgPSBGdW5jdGlvbi5wcm90b3R5cGUudG9TdHJpbmcsIHRyeUZ1bmN0aW9uT2JqZWN0ID0gZnVuY3Rpb24gdHJ5RnVuY3Rpb25PYmplY3QodmFsdWUpIHsgdHJ5IHsgZm5Ub1N0ci5jYWxsKHZhbHVlKTsgcmV0dXJuIHRydWU7IH0gY2F0Y2ggKGUpIHsgcmV0dXJuIGZhbHNlOyB9IH0sIGZuQ2xhc3MgPSAnW29iamVjdCBGdW5jdGlvbl0nLCBnZW5DbGFzcyA9ICdbb2JqZWN0IEdlbmVyYXRvckZ1bmN0aW9uXSc7IGlzQ2FsbGFibGUgPSBmdW5jdGlvbiBpc0NhbGxhYmxlKHZhbHVlKSB7IGlmICh0eXBlb2YgdmFsdWUgIT09ICdmdW5jdGlvbicpIHsgcmV0dXJuIGZhbHNlOyB9IGlmIChoYXNUb1N0cmluZ1RhZykgeyByZXR1cm4gdHJ5RnVuY3Rpb25PYmplY3QodmFsdWUpOyB9IHZhciBzdHJDbGFzcyA9IHRvX3N0cmluZy5jYWxsKHZhbHVlKTsgcmV0dXJuIHN0ckNsYXNzID09PSBmbkNsYXNzIHx8IHN0ckNsYXNzID09PSBnZW5DbGFzczsgfTtcbiAgICAgICAgICB2YXIgYXJyYXlfc2xpY2UgPSBBcnJheVByb3RvdHlwZS5zbGljZTtcbiAgICAgICAgICB2YXIgYXJyYXlfY29uY2F0ID0gQXJyYXlQcm90b3R5cGUuY29uY2F0O1xuICAgICAgICAgIHZhciBhcnJheV9wdXNoID0gQXJyYXlQcm90b3R5cGUucHVzaDtcbiAgICAgICAgICB2YXIgbWF4ID0gTWF0aC5tYXg7XG4gICAgICAgICAgLy8gL2FkZCBuZWNlc3NhcnkgZXM1LXNoaW0gdXRpbGl0aWVzXG5cbiAgICAgICAgICAvLyAxLiBMZXQgVGFyZ2V0IGJlIHRoZSB0aGlzIHZhbHVlLlxuICAgICAgICAgIHZhciB0YXJnZXQgPSB0aGlzO1xuICAgICAgICAgIC8vIDIuIElmIElzQ2FsbGFibGUoVGFyZ2V0KSBpcyBmYWxzZSwgdGhyb3cgYSBUeXBlRXJyb3IgZXhjZXB0aW9uLlxuICAgICAgICAgIGlmICghaXNDYWxsYWJsZSh0YXJnZXQpKSB7XG4gICAgICAgICAgICAgIHRocm93IG5ldyBUeXBlRXJyb3IoJ0Z1bmN0aW9uLnByb3RvdHlwZS5iaW5kIGNhbGxlZCBvbiBpbmNvbXBhdGlibGUgJyArIHRhcmdldCk7XG4gICAgICAgICAgfVxuICAgICAgICAgIC8vIDMuIExldCBBIGJlIGEgbmV3IChwb3NzaWJseSBlbXB0eSkgaW50ZXJuYWwgbGlzdCBvZiBhbGwgb2YgdGhlXG4gICAgICAgICAgLy8gICBhcmd1bWVudCB2YWx1ZXMgcHJvdmlkZWQgYWZ0ZXIgdGhpc0FyZyAoYXJnMSwgYXJnMiBldGMpLCBpbiBvcmRlci5cbiAgICAgICAgICAvLyBYWFggc2xpY2VkQXJncyB3aWxsIHN0YW5kIGluIGZvciBcIkFcIiBpZiB1c2VkXG4gICAgICAgICAgdmFyIGFyZ3MgPSBhcnJheV9zbGljZS5jYWxsKGFyZ3VtZW50cywgMSk7IC8vIGZvciBub3JtYWwgY2FsbFxuICAgICAgICAgIC8vIDQuIExldCBGIGJlIGEgbmV3IG5hdGl2ZSBFQ01BU2NyaXB0IG9iamVjdC5cbiAgICAgICAgICAvLyAxMS4gU2V0IHRoZSBbW1Byb3RvdHlwZV1dIGludGVybmFsIHByb3BlcnR5IG9mIEYgdG8gdGhlIHN0YW5kYXJkXG4gICAgICAgICAgLy8gICBidWlsdC1pbiBGdW5jdGlvbiBwcm90b3R5cGUgb2JqZWN0IGFzIHNwZWNpZmllZCBpbiAxNS4zLjMuMS5cbiAgICAgICAgICAvLyAxMi4gU2V0IHRoZSBbW0NhbGxdXSBpbnRlcm5hbCBwcm9wZXJ0eSBvZiBGIGFzIGRlc2NyaWJlZCBpblxuICAgICAgICAgIC8vICAgMTUuMy40LjUuMS5cbiAgICAgICAgICAvLyAxMy4gU2V0IHRoZSBbW0NvbnN0cnVjdF1dIGludGVybmFsIHByb3BlcnR5IG9mIEYgYXMgZGVzY3JpYmVkIGluXG4gICAgICAgICAgLy8gICAxNS4zLjQuNS4yLlxuICAgICAgICAgIC8vIDE0LiBTZXQgdGhlIFtbSGFzSW5zdGFuY2VdXSBpbnRlcm5hbCBwcm9wZXJ0eSBvZiBGIGFzIGRlc2NyaWJlZCBpblxuICAgICAgICAgIC8vICAgMTUuMy40LjUuMy5cbiAgICAgICAgICB2YXIgYm91bmQ7XG4gICAgICAgICAgdmFyIGJpbmRlciA9IGZ1bmN0aW9uICgpIHtcblxuICAgICAgICAgICAgICBpZiAodGhpcyBpbnN0YW5jZW9mIGJvdW5kKSB7XG4gICAgICAgICAgICAgICAgICAvLyAxNS4zLjQuNS4yIFtbQ29uc3RydWN0XV1cbiAgICAgICAgICAgICAgICAgIC8vIFdoZW4gdGhlIFtbQ29uc3RydWN0XV0gaW50ZXJuYWwgbWV0aG9kIG9mIGEgZnVuY3Rpb24gb2JqZWN0LFxuICAgICAgICAgICAgICAgICAgLy8gRiB0aGF0IHdhcyBjcmVhdGVkIHVzaW5nIHRoZSBiaW5kIGZ1bmN0aW9uIGlzIGNhbGxlZCB3aXRoIGFcbiAgICAgICAgICAgICAgICAgIC8vIGxpc3Qgb2YgYXJndW1lbnRzIEV4dHJhQXJncywgdGhlIGZvbGxvd2luZyBzdGVwcyBhcmUgdGFrZW46XG4gICAgICAgICAgICAgICAgICAvLyAxLiBMZXQgdGFyZ2V0IGJlIHRoZSB2YWx1ZSBvZiBGJ3MgW1tUYXJnZXRGdW5jdGlvbl1dXG4gICAgICAgICAgICAgICAgICAvLyAgIGludGVybmFsIHByb3BlcnR5LlxuICAgICAgICAgICAgICAgICAgLy8gMi4gSWYgdGFyZ2V0IGhhcyBubyBbW0NvbnN0cnVjdF1dIGludGVybmFsIG1ldGhvZCwgYVxuICAgICAgICAgICAgICAgICAgLy8gICBUeXBlRXJyb3IgZXhjZXB0aW9uIGlzIHRocm93bi5cbiAgICAgICAgICAgICAgICAgIC8vIDMuIExldCBib3VuZEFyZ3MgYmUgdGhlIHZhbHVlIG9mIEYncyBbW0JvdW5kQXJnc11dIGludGVybmFsXG4gICAgICAgICAgICAgICAgICAvLyAgIHByb3BlcnR5LlxuICAgICAgICAgICAgICAgICAgLy8gNC4gTGV0IGFyZ3MgYmUgYSBuZXcgbGlzdCBjb250YWluaW5nIHRoZSBzYW1lIHZhbHVlcyBhcyB0aGVcbiAgICAgICAgICAgICAgICAgIC8vICAgbGlzdCBib3VuZEFyZ3MgaW4gdGhlIHNhbWUgb3JkZXIgZm9sbG93ZWQgYnkgdGhlIHNhbWVcbiAgICAgICAgICAgICAgICAgIC8vICAgdmFsdWVzIGFzIHRoZSBsaXN0IEV4dHJhQXJncyBpbiB0aGUgc2FtZSBvcmRlci5cbiAgICAgICAgICAgICAgICAgIC8vIDUuIFJldHVybiB0aGUgcmVzdWx0IG9mIGNhbGxpbmcgdGhlIFtbQ29uc3RydWN0XV0gaW50ZXJuYWxcbiAgICAgICAgICAgICAgICAgIC8vICAgbWV0aG9kIG9mIHRhcmdldCBwcm92aWRpbmcgYXJncyBhcyB0aGUgYXJndW1lbnRzLlxuXG4gICAgICAgICAgICAgICAgICB2YXIgcmVzdWx0ID0gdGFyZ2V0LmFwcGx5KFxuICAgICAgICAgICAgICAgICAgICAgIHRoaXMsXG4gICAgICAgICAgICAgICAgICAgICAgYXJyYXlfY29uY2F0LmNhbGwoYXJncywgYXJyYXlfc2xpY2UuY2FsbChhcmd1bWVudHMpKVxuICAgICAgICAgICAgICAgICAgKTtcbiAgICAgICAgICAgICAgICAgIGlmICgkT2JqZWN0KHJlc3VsdCkgPT09IHJlc3VsdCkge1xuICAgICAgICAgICAgICAgICAgICAgIHJldHVybiByZXN1bHQ7XG4gICAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgICAgICByZXR1cm4gdGhpcztcblxuICAgICAgICAgICAgICB9IGVsc2Uge1xuICAgICAgICAgICAgICAgICAgLy8gMTUuMy40LjUuMSBbW0NhbGxdXVxuICAgICAgICAgICAgICAgICAgLy8gV2hlbiB0aGUgW1tDYWxsXV0gaW50ZXJuYWwgbWV0aG9kIG9mIGEgZnVuY3Rpb24gb2JqZWN0LCBGLFxuICAgICAgICAgICAgICAgICAgLy8gd2hpY2ggd2FzIGNyZWF0ZWQgdXNpbmcgdGhlIGJpbmQgZnVuY3Rpb24gaXMgY2FsbGVkIHdpdGggYVxuICAgICAgICAgICAgICAgICAgLy8gdGhpcyB2YWx1ZSBhbmQgYSBsaXN0IG9mIGFyZ3VtZW50cyBFeHRyYUFyZ3MsIHRoZSBmb2xsb3dpbmdcbiAgICAgICAgICAgICAgICAgIC8vIHN0ZXBzIGFyZSB0YWtlbjpcbiAgICAgICAgICAgICAgICAgIC8vIDEuIExldCBib3VuZEFyZ3MgYmUgdGhlIHZhbHVlIG9mIEYncyBbW0JvdW5kQXJnc11dIGludGVybmFsXG4gICAgICAgICAgICAgICAgICAvLyAgIHByb3BlcnR5LlxuICAgICAgICAgICAgICAgICAgLy8gMi4gTGV0IGJvdW5kVGhpcyBiZSB0aGUgdmFsdWUgb2YgRidzIFtbQm91bmRUaGlzXV0gaW50ZXJuYWxcbiAgICAgICAgICAgICAgICAgIC8vICAgcHJvcGVydHkuXG4gICAgICAgICAgICAgICAgICAvLyAzLiBMZXQgdGFyZ2V0IGJlIHRoZSB2YWx1ZSBvZiBGJ3MgW1tUYXJnZXRGdW5jdGlvbl1dIGludGVybmFsXG4gICAgICAgICAgICAgICAgICAvLyAgIHByb3BlcnR5LlxuICAgICAgICAgICAgICAgICAgLy8gNC4gTGV0IGFyZ3MgYmUgYSBuZXcgbGlzdCBjb250YWluaW5nIHRoZSBzYW1lIHZhbHVlcyBhcyB0aGVcbiAgICAgICAgICAgICAgICAgIC8vICAgbGlzdCBib3VuZEFyZ3MgaW4gdGhlIHNhbWUgb3JkZXIgZm9sbG93ZWQgYnkgdGhlIHNhbWVcbiAgICAgICAgICAgICAgICAgIC8vICAgdmFsdWVzIGFzIHRoZSBsaXN0IEV4dHJhQXJncyBpbiB0aGUgc2FtZSBvcmRlci5cbiAgICAgICAgICAgICAgICAgIC8vIDUuIFJldHVybiB0aGUgcmVzdWx0IG9mIGNhbGxpbmcgdGhlIFtbQ2FsbF1dIGludGVybmFsIG1ldGhvZFxuICAgICAgICAgICAgICAgICAgLy8gICBvZiB0YXJnZXQgcHJvdmlkaW5nIGJvdW5kVGhpcyBhcyB0aGUgdGhpcyB2YWx1ZSBhbmRcbiAgICAgICAgICAgICAgICAgIC8vICAgcHJvdmlkaW5nIGFyZ3MgYXMgdGhlIGFyZ3VtZW50cy5cblxuICAgICAgICAgICAgICAgICAgLy8gZXF1aXY6IHRhcmdldC5jYWxsKHRoaXMsIC4uLmJvdW5kQXJncywgLi4uYXJncylcbiAgICAgICAgICAgICAgICAgIHJldHVybiB0YXJnZXQuYXBwbHkoXG4gICAgICAgICAgICAgICAgICAgICAgdGhhdCxcbiAgICAgICAgICAgICAgICAgICAgICBhcnJheV9jb25jYXQuY2FsbChhcmdzLCBhcnJheV9zbGljZS5jYWxsKGFyZ3VtZW50cykpXG4gICAgICAgICAgICAgICAgICApO1xuXG4gICAgICAgICAgICAgIH1cblxuICAgICAgICAgIH07XG5cbiAgICAgICAgICAvLyAxNS4gSWYgdGhlIFtbQ2xhc3NdXSBpbnRlcm5hbCBwcm9wZXJ0eSBvZiBUYXJnZXQgaXMgXCJGdW5jdGlvblwiLCB0aGVuXG4gICAgICAgICAgLy8gICAgIGEuIExldCBMIGJlIHRoZSBsZW5ndGggcHJvcGVydHkgb2YgVGFyZ2V0IG1pbnVzIHRoZSBsZW5ndGggb2YgQS5cbiAgICAgICAgICAvLyAgICAgYi4gU2V0IHRoZSBsZW5ndGggb3duIHByb3BlcnR5IG9mIEYgdG8gZWl0aGVyIDAgb3IgTCwgd2hpY2hldmVyIGlzXG4gICAgICAgICAgLy8gICAgICAgbGFyZ2VyLlxuICAgICAgICAgIC8vIDE2LiBFbHNlIHNldCB0aGUgbGVuZ3RoIG93biBwcm9wZXJ0eSBvZiBGIHRvIDAuXG5cbiAgICAgICAgICB2YXIgYm91bmRMZW5ndGggPSBtYXgoMCwgdGFyZ2V0Lmxlbmd0aCAtIGFyZ3MubGVuZ3RoKTtcblxuICAgICAgICAgIC8vIDE3LiBTZXQgdGhlIGF0dHJpYnV0ZXMgb2YgdGhlIGxlbmd0aCBvd24gcHJvcGVydHkgb2YgRiB0byB0aGUgdmFsdWVzXG4gICAgICAgICAgLy8gICBzcGVjaWZpZWQgaW4gMTUuMy41LjEuXG4gICAgICAgICAgdmFyIGJvdW5kQXJncyA9IFtdO1xuICAgICAgICAgIGZvciAodmFyIGkgPSAwOyBpIDwgYm91bmRMZW5ndGg7IGkrKykge1xuICAgICAgICAgICAgICBhcnJheV9wdXNoLmNhbGwoYm91bmRBcmdzLCAnJCcgKyBpKTtcbiAgICAgICAgICB9XG5cbiAgICAgICAgICAvLyBYWFggQnVpbGQgYSBkeW5hbWljIGZ1bmN0aW9uIHdpdGggZGVzaXJlZCBhbW91bnQgb2YgYXJndW1lbnRzIGlzIHRoZSBvbmx5XG4gICAgICAgICAgLy8gd2F5IHRvIHNldCB0aGUgbGVuZ3RoIHByb3BlcnR5IG9mIGEgZnVuY3Rpb24uXG4gICAgICAgICAgLy8gSW4gZW52aXJvbm1lbnRzIHdoZXJlIENvbnRlbnQgU2VjdXJpdHkgUG9saWNpZXMgZW5hYmxlZCAoQ2hyb21lIGV4dGVuc2lvbnMsXG4gICAgICAgICAgLy8gZm9yIGV4LikgYWxsIHVzZSBvZiBldmFsIG9yIEZ1bmN0aW9uIGNvc3RydWN0b3IgdGhyb3dzIGFuIGV4Y2VwdGlvbi5cbiAgICAgICAgICAvLyBIb3dldmVyIGluIGFsbCBvZiB0aGVzZSBlbnZpcm9ubWVudHMgRnVuY3Rpb24ucHJvdG90eXBlLmJpbmQgZXhpc3RzXG4gICAgICAgICAgLy8gYW5kIHNvIHRoaXMgY29kZSB3aWxsIG5ldmVyIGJlIGV4ZWN1dGVkLlxuICAgICAgICAgIGJvdW5kID0gRnVuY3Rpb24oJ2JpbmRlcicsICdyZXR1cm4gZnVuY3Rpb24gKCcgKyBib3VuZEFyZ3Muam9pbignLCcpICsgJyl7IHJldHVybiBiaW5kZXIuYXBwbHkodGhpcywgYXJndW1lbnRzKTsgfScpKGJpbmRlcik7XG5cbiAgICAgICAgICBpZiAodGFyZ2V0LnByb3RvdHlwZSkge1xuICAgICAgICAgICAgICBFbXB0eS5wcm90b3R5cGUgPSB0YXJnZXQucHJvdG90eXBlO1xuICAgICAgICAgICAgICBib3VuZC5wcm90b3R5cGUgPSBuZXcgRW1wdHkoKTtcbiAgICAgICAgICAgICAgLy8gQ2xlYW4gdXAgZGFuZ2xpbmcgcmVmZXJlbmNlcy5cbiAgICAgICAgICAgICAgRW1wdHkucHJvdG90eXBlID0gbnVsbDtcbiAgICAgICAgICB9XG5cbiAgICAgICAgICAvLyBUT0RPXG4gICAgICAgICAgLy8gMTguIFNldCB0aGUgW1tFeHRlbnNpYmxlXV0gaW50ZXJuYWwgcHJvcGVydHkgb2YgRiB0byB0cnVlLlxuXG4gICAgICAgICAgLy8gVE9ET1xuICAgICAgICAgIC8vIDE5LiBMZXQgdGhyb3dlciBiZSB0aGUgW1tUaHJvd1R5cGVFcnJvcl1dIGZ1bmN0aW9uIE9iamVjdCAoMTMuMi4zKS5cbiAgICAgICAgICAvLyAyMC4gQ2FsbCB0aGUgW1tEZWZpbmVPd25Qcm9wZXJ0eV1dIGludGVybmFsIG1ldGhvZCBvZiBGIHdpdGhcbiAgICAgICAgICAvLyAgIGFyZ3VtZW50cyBcImNhbGxlclwiLCBQcm9wZXJ0eURlc2NyaXB0b3Ige1tbR2V0XV06IHRocm93ZXIsIFtbU2V0XV06XG4gICAgICAgICAgLy8gICB0aHJvd2VyLCBbW0VudW1lcmFibGVdXTogZmFsc2UsIFtbQ29uZmlndXJhYmxlXV06IGZhbHNlfSwgYW5kXG4gICAgICAgICAgLy8gICBmYWxzZS5cbiAgICAgICAgICAvLyAyMS4gQ2FsbCB0aGUgW1tEZWZpbmVPd25Qcm9wZXJ0eV1dIGludGVybmFsIG1ldGhvZCBvZiBGIHdpdGhcbiAgICAgICAgICAvLyAgIGFyZ3VtZW50cyBcImFyZ3VtZW50c1wiLCBQcm9wZXJ0eURlc2NyaXB0b3Ige1tbR2V0XV06IHRocm93ZXIsXG4gICAgICAgICAgLy8gICBbW1NldF1dOiB0aHJvd2VyLCBbW0VudW1lcmFibGVdXTogZmFsc2UsIFtbQ29uZmlndXJhYmxlXV06IGZhbHNlfSxcbiAgICAgICAgICAvLyAgIGFuZCBmYWxzZS5cblxuICAgICAgICAgIC8vIFRPRE9cbiAgICAgICAgICAvLyBOT1RFIEZ1bmN0aW9uIG9iamVjdHMgY3JlYXRlZCB1c2luZyBGdW5jdGlvbi5wcm90b3R5cGUuYmluZCBkbyBub3RcbiAgICAgICAgICAvLyBoYXZlIGEgcHJvdG90eXBlIHByb3BlcnR5IG9yIHRoZSBbW0NvZGVdXSwgW1tGb3JtYWxQYXJhbWV0ZXJzXV0sIGFuZFxuICAgICAgICAgIC8vIFtbU2NvcGVdXSBpbnRlcm5hbCBwcm9wZXJ0aWVzLlxuICAgICAgICAgIC8vIFhYWCBjYW4ndCBkZWxldGUgcHJvdG90eXBlIGluIHB1cmUtanMuXG5cbiAgICAgICAgICAvLyAyMi4gUmV0dXJuIEYuXG4gICAgICAgICAgcmV0dXJuIGJvdW5kO1xuICAgICAgfVxuICB9KTtcbn0pXG4uY2FsbCgnb2JqZWN0JyA9PT0gdHlwZW9mIHdpbmRvdyAmJiB3aW5kb3cgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBzZWxmICYmIHNlbGYgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBnbG9iYWwgJiYgZ2xvYmFsIHx8IHt9KTtcblxuKGZ1bmN0aW9uKHVuZGVmaW5lZCkge1xuXG4gICAgLy8gRGV0ZWN0aW9uIGZyb20gaHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlL21hc3Rlci9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9ET01Ub2tlbkxpc3QvZGV0ZWN0LmpzXG4gICAgdmFyIGRldGVjdCA9IChcbiAgICAgICdET01Ub2tlbkxpc3QnIGluIHRoaXMgJiYgKGZ1bmN0aW9uICh4KSB7XG4gICAgICAgIHJldHVybiAnY2xhc3NMaXN0JyBpbiB4ID8gIXguY2xhc3NMaXN0LnRvZ2dsZSgneCcsIGZhbHNlKSAmJiAheC5jbGFzc05hbWUgOiB0cnVlO1xuICAgICAgfSkoZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgneCcpKVxuICAgICk7XG5cbiAgICBpZiAoZGV0ZWN0KSByZXR1cm5cblxuICAgIC8vIFBvbHlmaWxsIGZyb20gaHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlL21hc3Rlci9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9ET01Ub2tlbkxpc3QvcG9seWZpbGwuanNcbiAgICAoZnVuY3Rpb24gKGdsb2JhbCkge1xuICAgICAgdmFyIG5hdGl2ZUltcGwgPSBcIkRPTVRva2VuTGlzdFwiIGluIGdsb2JhbCAmJiBnbG9iYWwuRE9NVG9rZW5MaXN0O1xuXG4gICAgICBpZiAoXG4gICAgICAgICAgIW5hdGl2ZUltcGwgfHxcbiAgICAgICAgICAoXG4gICAgICAgICAgICAhIWRvY3VtZW50LmNyZWF0ZUVsZW1lbnROUyAmJlxuICAgICAgICAgICAgISFkb2N1bWVudC5jcmVhdGVFbGVtZW50TlMoJ2h0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnJywgJ3N2ZycpICYmXG4gICAgICAgICAgICAhKGRvY3VtZW50LmNyZWF0ZUVsZW1lbnROUyhcImh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnXCIsIFwic3ZnXCIpLmNsYXNzTGlzdCBpbnN0YW5jZW9mIERPTVRva2VuTGlzdClcbiAgICAgICAgICApXG4gICAgICAgICkge1xuICAgICAgICBnbG9iYWwuRE9NVG9rZW5MaXN0ID0gKGZ1bmN0aW9uKCkgeyAvLyBlc2xpbnQtZGlzYWJsZS1saW5lIG5vLXVudXNlZC12YXJzXG4gICAgICAgICAgdmFyIGRwU3VwcG9ydCA9IHRydWU7XG4gICAgICAgICAgdmFyIGRlZmluZUdldHRlciA9IGZ1bmN0aW9uIChvYmplY3QsIG5hbWUsIGZuLCBjb25maWd1cmFibGUpIHtcbiAgICAgICAgICAgIGlmIChPYmplY3QuZGVmaW5lUHJvcGVydHkpXG4gICAgICAgICAgICAgIE9iamVjdC5kZWZpbmVQcm9wZXJ0eShvYmplY3QsIG5hbWUsIHtcbiAgICAgICAgICAgICAgICBjb25maWd1cmFibGU6IGZhbHNlID09PSBkcFN1cHBvcnQgPyB0cnVlIDogISFjb25maWd1cmFibGUsXG4gICAgICAgICAgICAgICAgZ2V0OiBmblxuICAgICAgICAgICAgICB9KTtcblxuICAgICAgICAgICAgZWxzZSBvYmplY3QuX19kZWZpbmVHZXR0ZXJfXyhuYW1lLCBmbik7XG4gICAgICAgICAgfTtcblxuICAgICAgICAgIC8qKiBFbnN1cmUgdGhlIGJyb3dzZXIgYWxsb3dzIE9iamVjdC5kZWZpbmVQcm9wZXJ0eSB0byBiZSB1c2VkIG9uIG5hdGl2ZSBKYXZhU2NyaXB0IG9iamVjdHMuICovXG4gICAgICAgICAgdHJ5IHtcbiAgICAgICAgICAgIGRlZmluZUdldHRlcih7fSwgXCJzdXBwb3J0XCIpO1xuICAgICAgICAgIH1cbiAgICAgICAgICBjYXRjaCAoZSkge1xuICAgICAgICAgICAgZHBTdXBwb3J0ID0gZmFsc2U7XG4gICAgICAgICAgfVxuXG5cbiAgICAgICAgICB2YXIgX0RPTVRva2VuTGlzdCA9IGZ1bmN0aW9uIChlbCwgcHJvcCkge1xuICAgICAgICAgICAgdmFyIHRoYXQgPSB0aGlzO1xuICAgICAgICAgICAgdmFyIHRva2VucyA9IFtdO1xuICAgICAgICAgICAgdmFyIHRva2VuTWFwID0ge307XG4gICAgICAgICAgICB2YXIgbGVuZ3RoID0gMDtcbiAgICAgICAgICAgIHZhciBtYXhMZW5ndGggPSAwO1xuICAgICAgICAgICAgdmFyIGFkZEluZGV4R2V0dGVyID0gZnVuY3Rpb24gKGkpIHtcbiAgICAgICAgICAgICAgZGVmaW5lR2V0dGVyKHRoYXQsIGksIGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgICAgICBwcmVvcCgpO1xuICAgICAgICAgICAgICAgIHJldHVybiB0b2tlbnNbaV07XG4gICAgICAgICAgICAgIH0sIGZhbHNlKTtcblxuICAgICAgICAgICAgfTtcbiAgICAgICAgICAgIHZhciByZWluZGV4ID0gZnVuY3Rpb24gKCkge1xuXG4gICAgICAgICAgICAgIC8qKiBEZWZpbmUgZ2V0dGVyIGZ1bmN0aW9ucyBmb3IgYXJyYXktbGlrZSBhY2Nlc3MgdG8gdGhlIHRva2VuTGlzdCdzIGNvbnRlbnRzLiAqL1xuICAgICAgICAgICAgICBpZiAobGVuZ3RoID49IG1heExlbmd0aClcbiAgICAgICAgICAgICAgICBmb3IgKDsgbWF4TGVuZ3RoIDwgbGVuZ3RoOyArK21heExlbmd0aCkge1xuICAgICAgICAgICAgICAgICAgYWRkSW5kZXhHZXR0ZXIobWF4TGVuZ3RoKTtcbiAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICB9O1xuXG4gICAgICAgICAgICAvKiogSGVscGVyIGZ1bmN0aW9uIGNhbGxlZCBhdCB0aGUgc3RhcnQgb2YgZWFjaCBjbGFzcyBtZXRob2QuIEludGVybmFsIHVzZSBvbmx5LiAqL1xuICAgICAgICAgICAgdmFyIHByZW9wID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgICB2YXIgZXJyb3I7XG4gICAgICAgICAgICAgIHZhciBpO1xuICAgICAgICAgICAgICB2YXIgYXJncyA9IGFyZ3VtZW50cztcbiAgICAgICAgICAgICAgdmFyIHJTcGFjZSA9IC9cXHMrLztcblxuICAgICAgICAgICAgICAvKiogVmFsaWRhdGUgdGhlIHRva2VuL3MgcGFzc2VkIHRvIGFuIGluc3RhbmNlIG1ldGhvZCwgaWYgYW55LiAqL1xuICAgICAgICAgICAgICBpZiAoYXJncy5sZW5ndGgpXG4gICAgICAgICAgICAgICAgZm9yIChpID0gMDsgaSA8IGFyZ3MubGVuZ3RoOyArK2kpXG4gICAgICAgICAgICAgICAgICBpZiAoclNwYWNlLnRlc3QoYXJnc1tpXSkpIHtcbiAgICAgICAgICAgICAgICAgICAgZXJyb3IgPSBuZXcgU3ludGF4RXJyb3IoJ1N0cmluZyBcIicgKyBhcmdzW2ldICsgJ1wiICcgKyBcImNvbnRhaW5zXCIgKyAnIGFuIGludmFsaWQgY2hhcmFjdGVyJyk7XG4gICAgICAgICAgICAgICAgICAgIGVycm9yLmNvZGUgPSA1O1xuICAgICAgICAgICAgICAgICAgICBlcnJvci5uYW1lID0gXCJJbnZhbGlkQ2hhcmFjdGVyRXJyb3JcIjtcbiAgICAgICAgICAgICAgICAgICAgdGhyb3cgZXJyb3I7XG4gICAgICAgICAgICAgICAgICB9XG5cblxuICAgICAgICAgICAgICAvKiogU3BsaXQgdGhlIG5ldyB2YWx1ZSBhcGFydCBieSB3aGl0ZXNwYWNlKi9cbiAgICAgICAgICAgICAgaWYgKHR5cGVvZiBlbFtwcm9wXSA9PT0gXCJvYmplY3RcIikge1xuICAgICAgICAgICAgICAgIHRva2VucyA9IChcIlwiICsgZWxbcHJvcF0uYmFzZVZhbCkucmVwbGFjZSgvXlxccyt8XFxzKyQvZywgXCJcIikuc3BsaXQoclNwYWNlKTtcbiAgICAgICAgICAgICAgfSBlbHNlIHtcbiAgICAgICAgICAgICAgICB0b2tlbnMgPSAoXCJcIiArIGVsW3Byb3BdKS5yZXBsYWNlKC9eXFxzK3xcXHMrJC9nLCBcIlwiKS5zcGxpdChyU3BhY2UpO1xuICAgICAgICAgICAgICB9XG5cbiAgICAgICAgICAgICAgLyoqIEF2b2lkIHRyZWF0aW5nIGJsYW5rIHN0cmluZ3MgYXMgc2luZ2xlLWl0ZW0gdG9rZW4gbGlzdHMgKi9cbiAgICAgICAgICAgICAgaWYgKFwiXCIgPT09IHRva2Vuc1swXSkgdG9rZW5zID0gW107XG5cbiAgICAgICAgICAgICAgLyoqIFJlcG9wdWxhdGUgdGhlIGludGVybmFsIHRva2VuIGxpc3RzICovXG4gICAgICAgICAgICAgIHRva2VuTWFwID0ge307XG4gICAgICAgICAgICAgIGZvciAoaSA9IDA7IGkgPCB0b2tlbnMubGVuZ3RoOyArK2kpXG4gICAgICAgICAgICAgICAgdG9rZW5NYXBbdG9rZW5zW2ldXSA9IHRydWU7XG4gICAgICAgICAgICAgIGxlbmd0aCA9IHRva2Vucy5sZW5ndGg7XG4gICAgICAgICAgICAgIHJlaW5kZXgoKTtcbiAgICAgICAgICAgIH07XG5cbiAgICAgICAgICAgIC8qKiBQb3B1bGF0ZSBvdXIgaW50ZXJuYWwgdG9rZW4gbGlzdCBpZiB0aGUgdGFyZ2V0ZWQgYXR0cmlidXRlIG9mIHRoZSBzdWJqZWN0IGVsZW1lbnQgaXNuJ3QgZW1wdHkuICovXG4gICAgICAgICAgICBwcmVvcCgpO1xuXG4gICAgICAgICAgICAvKiogUmV0dXJuIHRoZSBudW1iZXIgb2YgdG9rZW5zIGluIHRoZSB1bmRlcmx5aW5nIHN0cmluZy4gUmVhZC1vbmx5LiAqL1xuICAgICAgICAgICAgZGVmaW5lR2V0dGVyKHRoYXQsIFwibGVuZ3RoXCIsIGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgICAgcHJlb3AoKTtcbiAgICAgICAgICAgICAgcmV0dXJuIGxlbmd0aDtcbiAgICAgICAgICAgIH0pO1xuXG4gICAgICAgICAgICAvKiogT3ZlcnJpZGUgdGhlIGRlZmF1bHQgdG9TdHJpbmcvdG9Mb2NhbGVTdHJpbmcgbWV0aG9kcyB0byByZXR1cm4gYSBzcGFjZS1kZWxpbWl0ZWQgbGlzdCBvZiB0b2tlbnMgd2hlbiB0eXBlY2FzdC4gKi9cbiAgICAgICAgICAgIHRoYXQudG9Mb2NhbGVTdHJpbmcgPVxuICAgICAgICAgICAgICB0aGF0LnRvU3RyaW5nID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgICAgIHByZW9wKCk7XG4gICAgICAgICAgICAgICAgcmV0dXJuIHRva2Vucy5qb2luKFwiIFwiKTtcbiAgICAgICAgICAgICAgfTtcblxuICAgICAgICAgICAgdGhhdC5pdGVtID0gZnVuY3Rpb24gKGlkeCkge1xuICAgICAgICAgICAgICBwcmVvcCgpO1xuICAgICAgICAgICAgICByZXR1cm4gdG9rZW5zW2lkeF07XG4gICAgICAgICAgICB9O1xuXG4gICAgICAgICAgICB0aGF0LmNvbnRhaW5zID0gZnVuY3Rpb24gKHRva2VuKSB7XG4gICAgICAgICAgICAgIHByZW9wKCk7XG4gICAgICAgICAgICAgIHJldHVybiAhIXRva2VuTWFwW3Rva2VuXTtcbiAgICAgICAgICAgIH07XG5cbiAgICAgICAgICAgIHRoYXQuYWRkID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgICBwcmVvcC5hcHBseSh0aGF0LCBhcmdzID0gYXJndW1lbnRzKTtcblxuICAgICAgICAgICAgICBmb3IgKHZhciBhcmdzLCB0b2tlbiwgaSA9IDAsIGwgPSBhcmdzLmxlbmd0aDsgaSA8IGw7ICsraSkge1xuICAgICAgICAgICAgICAgIHRva2VuID0gYXJnc1tpXTtcbiAgICAgICAgICAgICAgICBpZiAoIXRva2VuTWFwW3Rva2VuXSkge1xuICAgICAgICAgICAgICAgICAgdG9rZW5zLnB1c2godG9rZW4pO1xuICAgICAgICAgICAgICAgICAgdG9rZW5NYXBbdG9rZW5dID0gdHJ1ZTtcbiAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgIH1cblxuICAgICAgICAgICAgICAvKiogVXBkYXRlIHRoZSB0YXJnZXRlZCBhdHRyaWJ1dGUgb2YgdGhlIGF0dGFjaGVkIGVsZW1lbnQgaWYgdGhlIHRva2VuIGxpc3QncyBjaGFuZ2VkLiAqL1xuICAgICAgICAgICAgICBpZiAobGVuZ3RoICE9PSB0b2tlbnMubGVuZ3RoKSB7XG4gICAgICAgICAgICAgICAgbGVuZ3RoID0gdG9rZW5zLmxlbmd0aCA+Pj4gMDtcbiAgICAgICAgICAgICAgICBpZiAodHlwZW9mIGVsW3Byb3BdID09PSBcIm9iamVjdFwiKSB7XG4gICAgICAgICAgICAgICAgICBlbFtwcm9wXS5iYXNlVmFsID0gdG9rZW5zLmpvaW4oXCIgXCIpO1xuICAgICAgICAgICAgICAgIH0gZWxzZSB7XG4gICAgICAgICAgICAgICAgICBlbFtwcm9wXSA9IHRva2Vucy5qb2luKFwiIFwiKTtcbiAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgICAgcmVpbmRleCgpO1xuICAgICAgICAgICAgICB9XG4gICAgICAgICAgICB9O1xuXG4gICAgICAgICAgICB0aGF0LnJlbW92ZSA9IGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgICAgcHJlb3AuYXBwbHkodGhhdCwgYXJncyA9IGFyZ3VtZW50cyk7XG5cbiAgICAgICAgICAgICAgLyoqIEJ1aWxkIGEgaGFzaCBvZiB0b2tlbiBuYW1lcyB0byBjb21wYXJlIGFnYWluc3Qgd2hlbiByZWNvbGxlY3Rpbmcgb3VyIHRva2VuIGxpc3QuICovXG4gICAgICAgICAgICAgIGZvciAodmFyIGFyZ3MsIGlnbm9yZSA9IHt9LCBpID0gMCwgdCA9IFtdOyBpIDwgYXJncy5sZW5ndGg7ICsraSkge1xuICAgICAgICAgICAgICAgIGlnbm9yZVthcmdzW2ldXSA9IHRydWU7XG4gICAgICAgICAgICAgICAgZGVsZXRlIHRva2VuTWFwW2FyZ3NbaV1dO1xuICAgICAgICAgICAgICB9XG5cbiAgICAgICAgICAgICAgLyoqIFJ1biB0aHJvdWdoIG91ciB0b2tlbnMgbGlzdCBhbmQgcmVhc3NpZ24gb25seSB0aG9zZSB0aGF0IGFyZW4ndCBkZWZpbmVkIGluIHRoZSBoYXNoIGRlY2xhcmVkIGFib3ZlLiAqL1xuICAgICAgICAgICAgICBmb3IgKGkgPSAwOyBpIDwgdG9rZW5zLmxlbmd0aDsgKytpKVxuICAgICAgICAgICAgICAgIGlmICghaWdub3JlW3Rva2Vuc1tpXV0pIHQucHVzaCh0b2tlbnNbaV0pO1xuXG4gICAgICAgICAgICAgIHRva2VucyA9IHQ7XG4gICAgICAgICAgICAgIGxlbmd0aCA9IHQubGVuZ3RoID4+PiAwO1xuXG4gICAgICAgICAgICAgIC8qKiBVcGRhdGUgdGhlIHRhcmdldGVkIGF0dHJpYnV0ZSBvZiB0aGUgYXR0YWNoZWQgZWxlbWVudC4gKi9cbiAgICAgICAgICAgICAgaWYgKHR5cGVvZiBlbFtwcm9wXSA9PT0gXCJvYmplY3RcIikge1xuICAgICAgICAgICAgICAgIGVsW3Byb3BdLmJhc2VWYWwgPSB0b2tlbnMuam9pbihcIiBcIik7XG4gICAgICAgICAgICAgIH0gZWxzZSB7XG4gICAgICAgICAgICAgICAgZWxbcHJvcF0gPSB0b2tlbnMuam9pbihcIiBcIik7XG4gICAgICAgICAgICAgIH1cbiAgICAgICAgICAgICAgcmVpbmRleCgpO1xuICAgICAgICAgICAgfTtcblxuICAgICAgICAgICAgdGhhdC50b2dnbGUgPSBmdW5jdGlvbiAodG9rZW4sIGZvcmNlKSB7XG4gICAgICAgICAgICAgIHByZW9wLmFwcGx5KHRoYXQsIFt0b2tlbl0pO1xuXG4gICAgICAgICAgICAgIC8qKiBUb2tlbiBzdGF0ZSdzIGJlaW5nIGZvcmNlZC4gKi9cbiAgICAgICAgICAgICAgaWYgKHVuZGVmaW5lZCAhPT0gZm9yY2UpIHtcbiAgICAgICAgICAgICAgICBpZiAoZm9yY2UpIHtcbiAgICAgICAgICAgICAgICAgIHRoYXQuYWRkKHRva2VuKTtcbiAgICAgICAgICAgICAgICAgIHJldHVybiB0cnVlO1xuICAgICAgICAgICAgICAgIH0gZWxzZSB7XG4gICAgICAgICAgICAgICAgICB0aGF0LnJlbW92ZSh0b2tlbik7XG4gICAgICAgICAgICAgICAgICByZXR1cm4gZmFsc2U7XG4gICAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgICB9XG5cbiAgICAgICAgICAgICAgLyoqIFRva2VuIGFscmVhZHkgZXhpc3RzIGluIHRva2VuTGlzdC4gUmVtb3ZlIGl0LCBhbmQgcmV0dXJuIEZBTFNFLiAqL1xuICAgICAgICAgICAgICBpZiAodG9rZW5NYXBbdG9rZW5dKSB7XG4gICAgICAgICAgICAgICAgdGhhdC5yZW1vdmUodG9rZW4pO1xuICAgICAgICAgICAgICAgIHJldHVybiBmYWxzZTtcbiAgICAgICAgICAgICAgfVxuXG4gICAgICAgICAgICAgIC8qKiBPdGhlcndpc2UsIGFkZCB0aGUgdG9rZW4gYW5kIHJldHVybiBUUlVFLiAqL1xuICAgICAgICAgICAgICB0aGF0LmFkZCh0b2tlbik7XG4gICAgICAgICAgICAgIHJldHVybiB0cnVlO1xuICAgICAgICAgICAgfTtcblxuICAgICAgICAgICAgcmV0dXJuIHRoYXQ7XG4gICAgICAgICAgfTtcblxuICAgICAgICAgIHJldHVybiBfRE9NVG9rZW5MaXN0O1xuICAgICAgICB9KCkpO1xuICAgICAgfVxuXG4gICAgICAvLyBBZGQgc2Vjb25kIGFyZ3VtZW50IHRvIG5hdGl2ZSBET01Ub2tlbkxpc3QudG9nZ2xlKCkgaWYgbmVjZXNzYXJ5XG4gICAgICAoZnVuY3Rpb24gKCkge1xuICAgICAgICB2YXIgZSA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoJ3NwYW4nKTtcbiAgICAgICAgaWYgKCEoJ2NsYXNzTGlzdCcgaW4gZSkpIHJldHVybjtcbiAgICAgICAgZS5jbGFzc0xpc3QudG9nZ2xlKCd4JywgZmFsc2UpO1xuICAgICAgICBpZiAoIWUuY2xhc3NMaXN0LmNvbnRhaW5zKCd4JykpIHJldHVybjtcbiAgICAgICAgZS5jbGFzc0xpc3QuY29uc3RydWN0b3IucHJvdG90eXBlLnRvZ2dsZSA9IGZ1bmN0aW9uIHRvZ2dsZSh0b2tlbiAvKiwgZm9yY2UqLykge1xuICAgICAgICAgIHZhciBmb3JjZSA9IGFyZ3VtZW50c1sxXTtcbiAgICAgICAgICBpZiAoZm9yY2UgPT09IHVuZGVmaW5lZCkge1xuICAgICAgICAgICAgdmFyIGFkZCA9ICF0aGlzLmNvbnRhaW5zKHRva2VuKTtcbiAgICAgICAgICAgIHRoaXNbYWRkID8gJ2FkZCcgOiAncmVtb3ZlJ10odG9rZW4pO1xuICAgICAgICAgICAgcmV0dXJuIGFkZDtcbiAgICAgICAgICB9XG4gICAgICAgICAgZm9yY2UgPSAhIWZvcmNlO1xuICAgICAgICAgIHRoaXNbZm9yY2UgPyAnYWRkJyA6ICdyZW1vdmUnXSh0b2tlbik7XG4gICAgICAgICAgcmV0dXJuIGZvcmNlO1xuICAgICAgICB9O1xuICAgICAgfSgpKTtcblxuICAgICAgLy8gQWRkIG11bHRpcGxlIGFyZ3VtZW50cyB0byBuYXRpdmUgRE9NVG9rZW5MaXN0LmFkZCgpIGlmIG5lY2Vzc2FyeVxuICAgICAgKGZ1bmN0aW9uICgpIHtcbiAgICAgICAgdmFyIGUgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdzcGFuJyk7XG4gICAgICAgIGlmICghKCdjbGFzc0xpc3QnIGluIGUpKSByZXR1cm47XG4gICAgICAgIGUuY2xhc3NMaXN0LmFkZCgnYScsICdiJyk7XG4gICAgICAgIGlmIChlLmNsYXNzTGlzdC5jb250YWlucygnYicpKSByZXR1cm47XG4gICAgICAgIHZhciBuYXRpdmUgPSBlLmNsYXNzTGlzdC5jb25zdHJ1Y3Rvci5wcm90b3R5cGUuYWRkO1xuICAgICAgICBlLmNsYXNzTGlzdC5jb25zdHJ1Y3Rvci5wcm90b3R5cGUuYWRkID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgIHZhciBhcmdzID0gYXJndW1lbnRzO1xuICAgICAgICAgIHZhciBsID0gYXJndW1lbnRzLmxlbmd0aDtcbiAgICAgICAgICBmb3IgKHZhciBpID0gMDsgaSA8IGw7IGkrKykge1xuICAgICAgICAgICAgbmF0aXZlLmNhbGwodGhpcywgYXJnc1tpXSk7XG4gICAgICAgICAgfVxuICAgICAgICB9O1xuICAgICAgfSgpKTtcblxuICAgICAgLy8gQWRkIG11bHRpcGxlIGFyZ3VtZW50cyB0byBuYXRpdmUgRE9NVG9rZW5MaXN0LnJlbW92ZSgpIGlmIG5lY2Vzc2FyeVxuICAgICAgKGZ1bmN0aW9uICgpIHtcbiAgICAgICAgdmFyIGUgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdzcGFuJyk7XG4gICAgICAgIGlmICghKCdjbGFzc0xpc3QnIGluIGUpKSByZXR1cm47XG4gICAgICAgIGUuY2xhc3NMaXN0LmFkZCgnYScpO1xuICAgICAgICBlLmNsYXNzTGlzdC5hZGQoJ2InKTtcbiAgICAgICAgZS5jbGFzc0xpc3QucmVtb3ZlKCdhJywgJ2InKTtcbiAgICAgICAgaWYgKCFlLmNsYXNzTGlzdC5jb250YWlucygnYicpKSByZXR1cm47XG4gICAgICAgIHZhciBuYXRpdmUgPSBlLmNsYXNzTGlzdC5jb25zdHJ1Y3Rvci5wcm90b3R5cGUucmVtb3ZlO1xuICAgICAgICBlLmNsYXNzTGlzdC5jb25zdHJ1Y3Rvci5wcm90b3R5cGUucmVtb3ZlID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgIHZhciBhcmdzID0gYXJndW1lbnRzO1xuICAgICAgICAgIHZhciBsID0gYXJndW1lbnRzLmxlbmd0aDtcbiAgICAgICAgICBmb3IgKHZhciBpID0gMDsgaSA8IGw7IGkrKykge1xuICAgICAgICAgICAgbmF0aXZlLmNhbGwodGhpcywgYXJnc1tpXSk7XG4gICAgICAgICAgfVxuICAgICAgICB9O1xuICAgICAgfSgpKTtcblxuICAgIH0odGhpcykpO1xuXG59KS5jYWxsKCdvYmplY3QnID09PSB0eXBlb2Ygd2luZG93ICYmIHdpbmRvdyB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIHNlbGYgJiYgc2VsZiB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIGdsb2JhbCAmJiBnbG9iYWwgfHwge30pO1xuXG4oZnVuY3Rpb24odW5kZWZpbmVkKSB7XG5cbi8vIERldGVjdGlvbiBmcm9tIGh0dHBzOi8vZ2l0aHViLmNvbS9GaW5hbmNpYWwtVGltZXMvcG9seWZpbGwtc2VydmljZS9ibG9iL21hc3Rlci9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9Eb2N1bWVudC9kZXRlY3QuanNcbnZhciBkZXRlY3QgPSAoXCJEb2N1bWVudFwiIGluIHRoaXMpO1xuXG5pZiAoZGV0ZWN0KSByZXR1cm5cblxuLy8gUG9seWZpbGwgZnJvbSBodHRwczovL2Nkbi5wb2x5ZmlsbC5pby92Mi9wb2x5ZmlsbC5qcz9mZWF0dXJlcz1Eb2N1bWVudCZmbGFncz1hbHdheXNcbmlmICgodHlwZW9mIFdvcmtlckdsb2JhbFNjb3BlID09PSBcInVuZGVmaW5lZFwiKSAmJiAodHlwZW9mIGltcG9ydFNjcmlwdHMgIT09IFwiZnVuY3Rpb25cIikpIHtcblxuXHRpZiAodGhpcy5IVE1MRG9jdW1lbnQpIHsgLy8gSUU4XG5cblx0XHQvLyBIVE1MRG9jdW1lbnQgaXMgYW4gZXh0ZW5zaW9uIG9mIERvY3VtZW50LiAgSWYgdGhlIGJyb3dzZXIgaGFzIEhUTUxEb2N1bWVudCBidXQgbm90IERvY3VtZW50LCB0aGUgZm9ybWVyIHdpbGwgc3VmZmljZSBhcyBhbiBhbGlhcyBmb3IgdGhlIGxhdHRlci5cblx0XHR0aGlzLkRvY3VtZW50ID0gdGhpcy5IVE1MRG9jdW1lbnQ7XG5cblx0fSBlbHNlIHtcblxuXHRcdC8vIENyZWF0ZSBhbiBlbXB0eSBmdW5jdGlvbiB0byBhY3QgYXMgdGhlIG1pc3NpbmcgY29uc3RydWN0b3IgZm9yIHRoZSBkb2N1bWVudCBvYmplY3QsIGF0dGFjaCB0aGUgZG9jdW1lbnQgb2JqZWN0IGFzIGl0cyBwcm90b3R5cGUuICBUaGUgZnVuY3Rpb24gbmVlZHMgdG8gYmUgYW5vbnltb3VzIGVsc2UgaXQgaXMgaG9pc3RlZCBhbmQgY2F1c2VzIHRoZSBmZWF0dXJlIGRldGVjdCB0byBwcmVtYXR1cmVseSBwYXNzLCBwcmV2ZW50aW5nIHRoZSBhc3NpZ25tZW50cyBiZWxvdyBiZWluZyBtYWRlLlxuXHRcdHRoaXMuRG9jdW1lbnQgPSB0aGlzLkhUTUxEb2N1bWVudCA9IGRvY3VtZW50LmNvbnN0cnVjdG9yID0gKG5ldyBGdW5jdGlvbigncmV0dXJuIGZ1bmN0aW9uIERvY3VtZW50KCkge30nKSgpKTtcblx0XHR0aGlzLkRvY3VtZW50LnByb3RvdHlwZSA9IGRvY3VtZW50O1xuXHR9XG59XG5cblxufSlcbi5jYWxsKCdvYmplY3QnID09PSB0eXBlb2Ygd2luZG93ICYmIHdpbmRvdyB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIHNlbGYgJiYgc2VsZiB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIGdsb2JhbCAmJiBnbG9iYWwgfHwge30pO1xuXG4oZnVuY3Rpb24odW5kZWZpbmVkKSB7XG5cbi8vIERldGVjdGlvbiBmcm9tIGh0dHBzOi8vZ2l0aHViLmNvbS9GaW5hbmNpYWwtVGltZXMvcG9seWZpbGwtc2VydmljZS9ibG9iL21hc3Rlci9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9FbGVtZW50L2RldGVjdC5qc1xudmFyIGRldGVjdCA9ICgnRWxlbWVudCcgaW4gdGhpcyAmJiAnSFRNTEVsZW1lbnQnIGluIHRoaXMpO1xuXG5pZiAoZGV0ZWN0KSByZXR1cm5cblxuLy8gUG9seWZpbGwgZnJvbSBodHRwczovL2Nkbi5wb2x5ZmlsbC5pby92Mi9wb2x5ZmlsbC5qcz9mZWF0dXJlcz1FbGVtZW50JmZsYWdzPWFsd2F5c1xuKGZ1bmN0aW9uICgpIHtcblxuXHQvLyBJRThcblx0aWYgKHdpbmRvdy5FbGVtZW50ICYmICF3aW5kb3cuSFRNTEVsZW1lbnQpIHtcblx0XHR3aW5kb3cuSFRNTEVsZW1lbnQgPSB3aW5kb3cuRWxlbWVudDtcblx0XHRyZXR1cm47XG5cdH1cblxuXHQvLyBjcmVhdGUgRWxlbWVudCBjb25zdHJ1Y3RvclxuXHR3aW5kb3cuRWxlbWVudCA9IHdpbmRvdy5IVE1MRWxlbWVudCA9IG5ldyBGdW5jdGlvbigncmV0dXJuIGZ1bmN0aW9uIEVsZW1lbnQoKSB7fScpKCk7XG5cblx0Ly8gZ2VuZXJhdGUgc2FuZGJveGVkIGlmcmFtZVxuXHR2YXIgdmJvZHkgPSBkb2N1bWVudC5hcHBlbmRDaGlsZChkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdib2R5JykpO1xuXHR2YXIgZnJhbWUgPSB2Ym9keS5hcHBlbmRDaGlsZChkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdpZnJhbWUnKSk7XG5cblx0Ly8gdXNlIHNhbmRib3hlZCBpZnJhbWUgdG8gcmVwbGljYXRlIEVsZW1lbnQgZnVuY3Rpb25hbGl0eVxuXHR2YXIgZnJhbWVEb2N1bWVudCA9IGZyYW1lLmNvbnRlbnRXaW5kb3cuZG9jdW1lbnQ7XG5cdHZhciBwcm90b3R5cGUgPSBFbGVtZW50LnByb3RvdHlwZSA9IGZyYW1lRG9jdW1lbnQuYXBwZW5kQ2hpbGQoZnJhbWVEb2N1bWVudC5jcmVhdGVFbGVtZW50KCcqJykpO1xuXHR2YXIgY2FjaGUgPSB7fTtcblxuXHQvLyBwb2x5ZmlsbCBFbGVtZW50LnByb3RvdHlwZSBvbiBhbiBlbGVtZW50XG5cdHZhciBzaGl2ID0gZnVuY3Rpb24gKGVsZW1lbnQsIGRlZXApIHtcblx0XHR2YXJcblx0XHRjaGlsZE5vZGVzID0gZWxlbWVudC5jaGlsZE5vZGVzIHx8IFtdLFxuXHRcdGluZGV4ID0gLTEsXG5cdFx0a2V5LCB2YWx1ZSwgY2hpbGROb2RlO1xuXG5cdFx0aWYgKGVsZW1lbnQubm9kZVR5cGUgPT09IDEgJiYgZWxlbWVudC5jb25zdHJ1Y3RvciAhPT0gRWxlbWVudCkge1xuXHRcdFx0ZWxlbWVudC5jb25zdHJ1Y3RvciA9IEVsZW1lbnQ7XG5cblx0XHRcdGZvciAoa2V5IGluIGNhY2hlKSB7XG5cdFx0XHRcdHZhbHVlID0gY2FjaGVba2V5XTtcblx0XHRcdFx0ZWxlbWVudFtrZXldID0gdmFsdWU7XG5cdFx0XHR9XG5cdFx0fVxuXG5cdFx0d2hpbGUgKGNoaWxkTm9kZSA9IGRlZXAgJiYgY2hpbGROb2Rlc1srK2luZGV4XSkge1xuXHRcdFx0c2hpdihjaGlsZE5vZGUsIGRlZXApO1xuXHRcdH1cblxuXHRcdHJldHVybiBlbGVtZW50O1xuXHR9O1xuXG5cdHZhciBlbGVtZW50cyA9IGRvY3VtZW50LmdldEVsZW1lbnRzQnlUYWdOYW1lKCcqJyk7XG5cdHZhciBuYXRpdmVDcmVhdGVFbGVtZW50ID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudDtcblx0dmFyIGludGVydmFsO1xuXHR2YXIgbG9vcExpbWl0ID0gMTAwO1xuXG5cdHByb3RvdHlwZS5hdHRhY2hFdmVudCgnb25wcm9wZXJ0eWNoYW5nZScsIGZ1bmN0aW9uIChldmVudCkge1xuXHRcdHZhclxuXHRcdHByb3BlcnR5TmFtZSA9IGV2ZW50LnByb3BlcnR5TmFtZSxcblx0XHRub25WYWx1ZSA9ICFjYWNoZS5oYXNPd25Qcm9wZXJ0eShwcm9wZXJ0eU5hbWUpLFxuXHRcdG5ld1ZhbHVlID0gcHJvdG90eXBlW3Byb3BlcnR5TmFtZV0sXG5cdFx0b2xkVmFsdWUgPSBjYWNoZVtwcm9wZXJ0eU5hbWVdLFxuXHRcdGluZGV4ID0gLTEsXG5cdFx0ZWxlbWVudDtcblxuXHRcdHdoaWxlIChlbGVtZW50ID0gZWxlbWVudHNbKytpbmRleF0pIHtcblx0XHRcdGlmIChlbGVtZW50Lm5vZGVUeXBlID09PSAxKSB7XG5cdFx0XHRcdGlmIChub25WYWx1ZSB8fCBlbGVtZW50W3Byb3BlcnR5TmFtZV0gPT09IG9sZFZhbHVlKSB7XG5cdFx0XHRcdFx0ZWxlbWVudFtwcm9wZXJ0eU5hbWVdID0gbmV3VmFsdWU7XG5cdFx0XHRcdH1cblx0XHRcdH1cblx0XHR9XG5cblx0XHRjYWNoZVtwcm9wZXJ0eU5hbWVdID0gbmV3VmFsdWU7XG5cdH0pO1xuXG5cdHByb3RvdHlwZS5jb25zdHJ1Y3RvciA9IEVsZW1lbnQ7XG5cblx0aWYgKCFwcm90b3R5cGUuaGFzQXR0cmlidXRlKSB7XG5cdFx0Ly8gPEVsZW1lbnQ+Lmhhc0F0dHJpYnV0ZVxuXHRcdHByb3RvdHlwZS5oYXNBdHRyaWJ1dGUgPSBmdW5jdGlvbiBoYXNBdHRyaWJ1dGUobmFtZSkge1xuXHRcdFx0cmV0dXJuIHRoaXMuZ2V0QXR0cmlidXRlKG5hbWUpICE9PSBudWxsO1xuXHRcdH07XG5cdH1cblxuXHQvLyBBcHBseSBFbGVtZW50IHByb3RvdHlwZSB0byB0aGUgcHJlLWV4aXN0aW5nIERPTSBhcyBzb29uIGFzIHRoZSBib2R5IGVsZW1lbnQgYXBwZWFycy5cblx0ZnVuY3Rpb24gYm9keUNoZWNrKCkge1xuXHRcdGlmICghKGxvb3BMaW1pdC0tKSkgY2xlYXJUaW1lb3V0KGludGVydmFsKTtcblx0XHRpZiAoZG9jdW1lbnQuYm9keSAmJiAhZG9jdW1lbnQuYm9keS5wcm90b3R5cGUgJiYgLyhjb21wbGV0ZXxpbnRlcmFjdGl2ZSkvLnRlc3QoZG9jdW1lbnQucmVhZHlTdGF0ZSkpIHtcblx0XHRcdHNoaXYoZG9jdW1lbnQsIHRydWUpO1xuXHRcdFx0aWYgKGludGVydmFsICYmIGRvY3VtZW50LmJvZHkucHJvdG90eXBlKSBjbGVhclRpbWVvdXQoaW50ZXJ2YWwpO1xuXHRcdFx0cmV0dXJuICghIWRvY3VtZW50LmJvZHkucHJvdG90eXBlKTtcblx0XHR9XG5cdFx0cmV0dXJuIGZhbHNlO1xuXHR9XG5cdGlmICghYm9keUNoZWNrKCkpIHtcblx0XHRkb2N1bWVudC5vbnJlYWR5c3RhdGVjaGFuZ2UgPSBib2R5Q2hlY2s7XG5cdFx0aW50ZXJ2YWwgPSBzZXRJbnRlcnZhbChib2R5Q2hlY2ssIDI1KTtcblx0fVxuXG5cdC8vIEFwcGx5IHRvIGFueSBuZXcgZWxlbWVudHMgY3JlYXRlZCBhZnRlciBsb2FkXG5cdGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQgPSBmdW5jdGlvbiBjcmVhdGVFbGVtZW50KG5vZGVOYW1lKSB7XG5cdFx0dmFyIGVsZW1lbnQgPSBuYXRpdmVDcmVhdGVFbGVtZW50KFN0cmluZyhub2RlTmFtZSkudG9Mb3dlckNhc2UoKSk7XG5cdFx0cmV0dXJuIHNoaXYoZWxlbWVudCk7XG5cdH07XG5cblx0Ly8gcmVtb3ZlIHNhbmRib3hlZCBpZnJhbWVcblx0ZG9jdW1lbnQucmVtb3ZlQ2hpbGQodmJvZHkpO1xufSgpKTtcblxufSlcbi5jYWxsKCdvYmplY3QnID09PSB0eXBlb2Ygd2luZG93ICYmIHdpbmRvdyB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIHNlbGYgJiYgc2VsZiB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIGdsb2JhbCAmJiBnbG9iYWwgfHwge30pO1xuXG4oZnVuY3Rpb24odW5kZWZpbmVkKSB7XG5cbiAgICAvLyBEZXRlY3Rpb24gZnJvbSBodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vRmluYW5jaWFsLVRpbWVzL3BvbHlmaWxsLXNlcnZpY2UvODcxN2E5ZTA0YWM3YWZmOTliNDk4MGZiZWRlYWQ5ODAzNmIwOTI5YS9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9FbGVtZW50L3Byb3RvdHlwZS9jbGFzc0xpc3QvZGV0ZWN0LmpzXG4gICAgdmFyIGRldGVjdCA9IChcbiAgICAgICdkb2N1bWVudCcgaW4gdGhpcyAmJiBcImNsYXNzTGlzdFwiIGluIGRvY3VtZW50LmRvY3VtZW50RWxlbWVudCAmJiAnRWxlbWVudCcgaW4gdGhpcyAmJiAnY2xhc3NMaXN0JyBpbiBFbGVtZW50LnByb3RvdHlwZSAmJiAoZnVuY3Rpb24gKCkge1xuICAgICAgICB2YXIgZSA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoJ3NwYW4nKTtcbiAgICAgICAgZS5jbGFzc0xpc3QuYWRkKCdhJywgJ2InKTtcbiAgICAgICAgcmV0dXJuIGUuY2xhc3NMaXN0LmNvbnRhaW5zKCdiJyk7XG4gICAgICB9KCkpXG4gICAgKTtcblxuICAgIGlmIChkZXRlY3QpIHJldHVyblxuXG4gICAgLy8gUG9seWZpbGwgZnJvbSBodHRwczovL2Nkbi5wb2x5ZmlsbC5pby92Mi9wb2x5ZmlsbC5qcz9mZWF0dXJlcz1FbGVtZW50LnByb3RvdHlwZS5jbGFzc0xpc3QmZmxhZ3M9YWx3YXlzXG4gICAgKGZ1bmN0aW9uIChnbG9iYWwpIHtcbiAgICAgIHZhciBkcFN1cHBvcnQgPSB0cnVlO1xuICAgICAgdmFyIGRlZmluZUdldHRlciA9IGZ1bmN0aW9uIChvYmplY3QsIG5hbWUsIGZuLCBjb25maWd1cmFibGUpIHtcbiAgICAgICAgaWYgKE9iamVjdC5kZWZpbmVQcm9wZXJ0eSlcbiAgICAgICAgICBPYmplY3QuZGVmaW5lUHJvcGVydHkob2JqZWN0LCBuYW1lLCB7XG4gICAgICAgICAgICBjb25maWd1cmFibGU6IGZhbHNlID09PSBkcFN1cHBvcnQgPyB0cnVlIDogISFjb25maWd1cmFibGUsXG4gICAgICAgICAgICBnZXQ6IGZuXG4gICAgICAgICAgfSk7XG5cbiAgICAgICAgZWxzZSBvYmplY3QuX19kZWZpbmVHZXR0ZXJfXyhuYW1lLCBmbik7XG4gICAgICB9O1xuICAgICAgLyoqIEVuc3VyZSB0aGUgYnJvd3NlciBhbGxvd3MgT2JqZWN0LmRlZmluZVByb3BlcnR5IHRvIGJlIHVzZWQgb24gbmF0aXZlIEphdmFTY3JpcHQgb2JqZWN0cy4gKi9cbiAgICAgIHRyeSB7XG4gICAgICAgIGRlZmluZUdldHRlcih7fSwgXCJzdXBwb3J0XCIpO1xuICAgICAgfVxuICAgICAgY2F0Y2ggKGUpIHtcbiAgICAgICAgZHBTdXBwb3J0ID0gZmFsc2U7XG4gICAgICB9XG4gICAgICAvKiogUG9seWZpbGxzIGEgcHJvcGVydHkgd2l0aCBhIERPTVRva2VuTGlzdCAqL1xuICAgICAgdmFyIGFkZFByb3AgPSBmdW5jdGlvbiAobywgbmFtZSwgYXR0cikge1xuXG4gICAgICAgIGRlZmluZUdldHRlcihvLnByb3RvdHlwZSwgbmFtZSwgZnVuY3Rpb24gKCkge1xuICAgICAgICAgIHZhciB0b2tlbkxpc3Q7XG5cbiAgICAgICAgICB2YXIgVEhJUyA9IHRoaXMsXG5cbiAgICAgICAgICAvKiogUHJldmVudCB0aGlzIGZyb20gZmlyaW5nIHR3aWNlIGZvciBzb21lIHJlYXNvbi4gV2hhdCB0aGUgaGVsbCwgSUUuICovXG4gICAgICAgICAgZ2liYmVyaXNoUHJvcGVydHkgPSBcIl9fZGVmaW5lR2V0dGVyX19cIiArIFwiREVGSU5FX1BST1BFUlRZXCIgKyBuYW1lO1xuICAgICAgICAgIGlmKFRISVNbZ2liYmVyaXNoUHJvcGVydHldKSByZXR1cm4gdG9rZW5MaXN0O1xuICAgICAgICAgIFRISVNbZ2liYmVyaXNoUHJvcGVydHldID0gdHJ1ZTtcblxuICAgICAgICAgIC8qKlxuICAgICAgICAgICAqIElFOCBjYW4ndCBkZWZpbmUgcHJvcGVydGllcyBvbiBuYXRpdmUgSmF2YVNjcmlwdCBvYmplY3RzLCBzbyB3ZSdsbCB1c2UgYSBkdW1iIGhhY2sgaW5zdGVhZC5cbiAgICAgICAgICAgKlxuICAgICAgICAgICAqIFdoYXQgdGhpcyBpcyBkb2luZyBpcyBjcmVhdGluZyBhIGR1bW15IGVsZW1lbnQgKFwicmVmbGVjdGlvblwiKSBpbnNpZGUgYSBkZXRhY2hlZCBwaGFudG9tIG5vZGUgKFwibWlycm9yXCIpXG4gICAgICAgICAgICogdGhhdCBzZXJ2ZXMgYXMgdGhlIHRhcmdldCBvZiBPYmplY3QuZGVmaW5lUHJvcGVydHkgaW5zdGVhZC4gV2hpbGUgd2UgY291bGQgc2ltcGx5IHVzZSB0aGUgc3ViamVjdCBIVE1MXG4gICAgICAgICAgICogZWxlbWVudCBpbnN0ZWFkLCB0aGlzIHdvdWxkIGNvbmZsaWN0IHdpdGggZWxlbWVudCB0eXBlcyB3aGljaCB1c2UgaW5kZXhlZCBwcm9wZXJ0aWVzIChzdWNoIGFzIGZvcm1zIGFuZFxuICAgICAgICAgICAqIHNlbGVjdCBsaXN0cykuXG4gICAgICAgICAgICovXG4gICAgICAgICAgaWYgKGZhbHNlID09PSBkcFN1cHBvcnQpIHtcblxuICAgICAgICAgICAgdmFyIHZpc2FnZTtcbiAgICAgICAgICAgIHZhciBtaXJyb3IgPSBhZGRQcm9wLm1pcnJvciB8fCBkb2N1bWVudC5jcmVhdGVFbGVtZW50KFwiZGl2XCIpO1xuICAgICAgICAgICAgdmFyIHJlZmxlY3Rpb25zID0gbWlycm9yLmNoaWxkTm9kZXM7XG4gICAgICAgICAgICB2YXIgbCA9IHJlZmxlY3Rpb25zLmxlbmd0aDtcblxuICAgICAgICAgICAgZm9yICh2YXIgaSA9IDA7IGkgPCBsOyArK2kpXG4gICAgICAgICAgICAgIGlmIChyZWZsZWN0aW9uc1tpXS5fUiA9PT0gVEhJUykge1xuICAgICAgICAgICAgICAgIHZpc2FnZSA9IHJlZmxlY3Rpb25zW2ldO1xuICAgICAgICAgICAgICAgIGJyZWFrO1xuICAgICAgICAgICAgICB9XG5cbiAgICAgICAgICAgIC8qKiBDb3VsZG4ndCBmaW5kIGFuIGVsZW1lbnQncyByZWZsZWN0aW9uIGluc2lkZSB0aGUgbWlycm9yLiBNYXRlcmlhbGlzZSBvbmUuICovXG4gICAgICAgICAgICB2aXNhZ2UgfHwgKHZpc2FnZSA9IG1pcnJvci5hcHBlbmRDaGlsZChkb2N1bWVudC5jcmVhdGVFbGVtZW50KFwiZGl2XCIpKSk7XG5cbiAgICAgICAgICAgIHRva2VuTGlzdCA9IERPTVRva2VuTGlzdC5jYWxsKHZpc2FnZSwgVEhJUywgYXR0cik7XG4gICAgICAgICAgfSBlbHNlIHRva2VuTGlzdCA9IG5ldyBET01Ub2tlbkxpc3QoVEhJUywgYXR0cik7XG5cbiAgICAgICAgICBkZWZpbmVHZXR0ZXIoVEhJUywgbmFtZSwgZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgcmV0dXJuIHRva2VuTGlzdDtcbiAgICAgICAgICB9KTtcbiAgICAgICAgICBkZWxldGUgVEhJU1tnaWJiZXJpc2hQcm9wZXJ0eV07XG5cbiAgICAgICAgICByZXR1cm4gdG9rZW5MaXN0O1xuICAgICAgICB9LCB0cnVlKTtcbiAgICAgIH07XG5cbiAgICAgIGFkZFByb3AoZ2xvYmFsLkVsZW1lbnQsIFwiY2xhc3NMaXN0XCIsIFwiY2xhc3NOYW1lXCIpO1xuICAgICAgYWRkUHJvcChnbG9iYWwuSFRNTEVsZW1lbnQsIFwiY2xhc3NMaXN0XCIsIFwiY2xhc3NOYW1lXCIpO1xuICAgICAgYWRkUHJvcChnbG9iYWwuSFRNTExpbmtFbGVtZW50LCBcInJlbExpc3RcIiwgXCJyZWxcIik7XG4gICAgICBhZGRQcm9wKGdsb2JhbC5IVE1MQW5jaG9yRWxlbWVudCwgXCJyZWxMaXN0XCIsIFwicmVsXCIpO1xuICAgICAgYWRkUHJvcChnbG9iYWwuSFRNTEFyZWFFbGVtZW50LCBcInJlbExpc3RcIiwgXCJyZWxcIik7XG4gICAgfSh0aGlzKSk7XG5cbn0pLmNhbGwoJ29iamVjdCcgPT09IHR5cGVvZiB3aW5kb3cgJiYgd2luZG93IHx8ICdvYmplY3QnID09PSB0eXBlb2Ygc2VsZiAmJiBzZWxmIHx8ICdvYmplY3QnID09PSB0eXBlb2YgZ2xvYmFsICYmIGdsb2JhbCB8fCB7fSk7XG5cbmZ1bmN0aW9uIEFjY29yZGlvbiAoJG1vZHVsZSkge1xuICB0aGlzLiRtb2R1bGUgPSAkbW9kdWxlO1xuICB0aGlzLm1vZHVsZUlkID0gJG1vZHVsZS5nZXRBdHRyaWJ1dGUoJ2lkJyk7XG4gIHRoaXMuJHNlY3Rpb25zID0gJG1vZHVsZS5xdWVyeVNlbGVjdG9yQWxsKCcuZ292dWstYWNjb3JkaW9uX19zZWN0aW9uJyk7XG4gIHRoaXMuJG9wZW5BbGxCdXR0b24gPSAnJztcbiAgdGhpcy5icm93c2VyU3VwcG9ydHNTZXNzaW9uU3RvcmFnZSA9IGhlbHBlci5jaGVja0ZvclNlc3Npb25TdG9yYWdlKCk7XG5cbiAgdGhpcy5jb250cm9sc0NsYXNzID0gJ2dvdnVrLWFjY29yZGlvbl9fY29udHJvbHMnO1xuICB0aGlzLm9wZW5BbGxDbGFzcyA9ICdnb3Z1ay1hY2NvcmRpb25fX29wZW4tYWxsJztcbiAgdGhpcy5pY29uQ2xhc3MgPSAnZ292dWstYWNjb3JkaW9uX19pY29uJztcblxuICB0aGlzLnNlY3Rpb25IZWFkZXJDbGFzcyA9ICdnb3Z1ay1hY2NvcmRpb25fX3NlY3Rpb24taGVhZGVyJztcbiAgdGhpcy5zZWN0aW9uSGVhZGVyRm9jdXNlZENsYXNzID0gJ2dvdnVrLWFjY29yZGlvbl9fc2VjdGlvbi1oZWFkZXItLWZvY3VzZWQnO1xuICB0aGlzLnNlY3Rpb25IZWFkaW5nQ2xhc3MgPSAnZ292dWstYWNjb3JkaW9uX19zZWN0aW9uLWhlYWRpbmcnO1xuICB0aGlzLnNlY3Rpb25TdW1tYXJ5Q2xhc3MgPSAnZ292dWstYWNjb3JkaW9uX19zZWN0aW9uLXN1bW1hcnknO1xuICB0aGlzLnNlY3Rpb25CdXR0b25DbGFzcyA9ICdnb3Z1ay1hY2NvcmRpb25fX3NlY3Rpb24tYnV0dG9uJztcbiAgdGhpcy5zZWN0aW9uRXhwYW5kZWRDbGFzcyA9ICdnb3Z1ay1hY2NvcmRpb25fX3NlY3Rpb24tLWV4cGFuZGVkJztcbn1cblxuLy8gSW5pdGlhbGl6ZSBjb21wb25lbnRcbkFjY29yZGlvbi5wcm90b3R5cGUuaW5pdCA9IGZ1bmN0aW9uICgpIHtcbiAgLy8gQ2hlY2sgZm9yIG1vZHVsZVxuICBpZiAoIXRoaXMuJG1vZHVsZSkge1xuICAgIHJldHVyblxuICB9XG5cbiAgdGhpcy5pbml0Q29udHJvbHMoKTtcblxuICB0aGlzLmluaXRTZWN0aW9uSGVhZGVycygpO1xuXG4gIC8vIFNlZSBpZiBcIk9wZW4gYWxsXCIgYnV0dG9uIHRleHQgc2hvdWxkIGJlIHVwZGF0ZWRcbiAgdmFyIGFyZUFsbFNlY3Rpb25zT3BlbiA9IHRoaXMuY2hlY2tJZkFsbFNlY3Rpb25zT3BlbigpO1xuICB0aGlzLnVwZGF0ZU9wZW5BbGxCdXR0b24oYXJlQWxsU2VjdGlvbnNPcGVuKTtcbn07XG5cbi8vIEluaXRpYWxpc2UgY29udHJvbHMgYW5kIHNldCBhdHRyaWJ1dGVzXG5BY2NvcmRpb24ucHJvdG90eXBlLmluaXRDb250cm9scyA9IGZ1bmN0aW9uICgpIHtcbiAgLy8gQ3JlYXRlIFwiT3BlbiBhbGxcIiBidXR0b24gYW5kIHNldCBhdHRyaWJ1dGVzXG4gIHRoaXMuJG9wZW5BbGxCdXR0b24gPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdidXR0b24nKTtcbiAgdGhpcy4kb3BlbkFsbEJ1dHRvbi5zZXRBdHRyaWJ1dGUoJ3R5cGUnLCAnYnV0dG9uJyk7XG4gIHRoaXMuJG9wZW5BbGxCdXR0b24uaW5uZXJIVE1MID0gJ09wZW4gYWxsIDxzcGFuIGNsYXNzPVwiZ292dWstdmlzdWFsbHktaGlkZGVuXCI+c2VjdGlvbnM8L3NwYW4+JztcbiAgdGhpcy4kb3BlbkFsbEJ1dHRvbi5zZXRBdHRyaWJ1dGUoJ2NsYXNzJywgdGhpcy5vcGVuQWxsQ2xhc3MpO1xuICB0aGlzLiRvcGVuQWxsQnV0dG9uLnNldEF0dHJpYnV0ZSgnYXJpYS1leHBhbmRlZCcsICdmYWxzZScpO1xuICB0aGlzLiRvcGVuQWxsQnV0dG9uLnNldEF0dHJpYnV0ZSgndHlwZScsICdidXR0b24nKTtcblxuICAvLyBDcmVhdGUgY29udHJvbCB3cmFwcGVyIGFuZCBhZGQgY29udHJvbHMgdG8gaXRcbiAgdmFyIGFjY29yZGlvbkNvbnRyb2xzID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnZGl2Jyk7XG4gIGFjY29yZGlvbkNvbnRyb2xzLnNldEF0dHJpYnV0ZSgnY2xhc3MnLCB0aGlzLmNvbnRyb2xzQ2xhc3MpO1xuICBhY2NvcmRpb25Db250cm9scy5hcHBlbmRDaGlsZCh0aGlzLiRvcGVuQWxsQnV0dG9uKTtcbiAgdGhpcy4kbW9kdWxlLmluc2VydEJlZm9yZShhY2NvcmRpb25Db250cm9scywgdGhpcy4kbW9kdWxlLmZpcnN0Q2hpbGQpO1xuXG4gIC8vIEhhbmRsZSBldmVudHMgZm9yIHRoZSBjb250cm9sc1xuICB0aGlzLiRvcGVuQWxsQnV0dG9uLmFkZEV2ZW50TGlzdGVuZXIoJ2NsaWNrJywgdGhpcy5vbk9wZW5PckNsb3NlQWxsVG9nZ2xlLmJpbmQodGhpcykpO1xufTtcblxuLy8gSW5pdGlhbGlzZSBzZWN0aW9uIGhlYWRlcnNcbkFjY29yZGlvbi5wcm90b3R5cGUuaW5pdFNlY3Rpb25IZWFkZXJzID0gZnVuY3Rpb24gKCkge1xuICAvLyBMb29wIHRocm91Z2ggc2VjdGlvbiBoZWFkZXJzXG4gIG5vZGVMaXN0Rm9yRWFjaCh0aGlzLiRzZWN0aW9ucywgZnVuY3Rpb24gKCRzZWN0aW9uLCBpKSB7XG4gICAgLy8gU2V0IGhlYWRlciBhdHRyaWJ1dGVzXG4gICAgdmFyIGhlYWRlciA9ICRzZWN0aW9uLnF1ZXJ5U2VsZWN0b3IoJy4nICsgdGhpcy5zZWN0aW9uSGVhZGVyQ2xhc3MpO1xuICAgIHRoaXMuaW5pdEhlYWRlckF0dHJpYnV0ZXMoaGVhZGVyLCBpKTtcblxuICAgIHRoaXMuc2V0RXhwYW5kZWQodGhpcy5pc0V4cGFuZGVkKCRzZWN0aW9uKSwgJHNlY3Rpb24pO1xuXG4gICAgLy8gSGFuZGxlIGV2ZW50c1xuICAgIGhlYWRlci5hZGRFdmVudExpc3RlbmVyKCdjbGljaycsIHRoaXMub25TZWN0aW9uVG9nZ2xlLmJpbmQodGhpcywgJHNlY3Rpb24pKTtcblxuICAgIC8vIFNlZSBpZiB0aGVyZSBpcyBhbnkgc3RhdGUgc3RvcmVkIGluIHNlc3Npb25TdG9yYWdlIGFuZCBzZXQgdGhlIHNlY3Rpb25zIHRvXG4gICAgLy8gb3BlbiBvciBjbG9zZWQuXG4gICAgdGhpcy5zZXRJbml0aWFsU3RhdGUoJHNlY3Rpb24pO1xuICB9LmJpbmQodGhpcykpO1xufTtcblxuLy8gU2V0IGluZGl2aWR1YWwgaGVhZGVyIGF0dHJpYnV0ZXNcbkFjY29yZGlvbi5wcm90b3R5cGUuaW5pdEhlYWRlckF0dHJpYnV0ZXMgPSBmdW5jdGlvbiAoJGhlYWRlcldyYXBwZXIsIGluZGV4KSB7XG4gIHZhciAkbW9kdWxlID0gdGhpcztcbiAgdmFyICRzcGFuID0gJGhlYWRlcldyYXBwZXIucXVlcnlTZWxlY3RvcignLicgKyB0aGlzLnNlY3Rpb25CdXR0b25DbGFzcyk7XG4gIHZhciAkaGVhZGluZyA9ICRoZWFkZXJXcmFwcGVyLnF1ZXJ5U2VsZWN0b3IoJy4nICsgdGhpcy5zZWN0aW9uSGVhZGluZ0NsYXNzKTtcbiAgdmFyICRzdW1tYXJ5ID0gJGhlYWRlcldyYXBwZXIucXVlcnlTZWxlY3RvcignLicgKyB0aGlzLnNlY3Rpb25TdW1tYXJ5Q2xhc3MpO1xuXG4gIC8vIENvcHkgZXhpc3Rpbmcgc3BhbiBlbGVtZW50IHRvIGFuIGFjdHVhbCBidXR0b24gZWxlbWVudCwgZm9yIGltcHJvdmVkIGFjY2Vzc2liaWxpdHkuXG4gIHZhciAkYnV0dG9uID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnYnV0dG9uJyk7XG4gICRidXR0b24uc2V0QXR0cmlidXRlKCd0eXBlJywgJ2J1dHRvbicpO1xuICAkYnV0dG9uLnNldEF0dHJpYnV0ZSgnaWQnLCB0aGlzLm1vZHVsZUlkICsgJy1oZWFkaW5nLScgKyAoaW5kZXggKyAxKSk7XG4gICRidXR0b24uc2V0QXR0cmlidXRlKCdhcmlhLWNvbnRyb2xzJywgdGhpcy5tb2R1bGVJZCArICctY29udGVudC0nICsgKGluZGV4ICsgMSkpO1xuXG4gIC8vIENvcHkgYWxsIGF0dHJpYnV0ZXMgKGh0dHBzOi8vZGV2ZWxvcGVyLm1vemlsbGEub3JnL2VuLVVTL2RvY3MvV2ViL0FQSS9FbGVtZW50L2F0dHJpYnV0ZXMpIGZyb20gJHNwYW4gdG8gJGJ1dHRvblxuICBmb3IgKHZhciBpID0gMDsgaSA8ICRzcGFuLmF0dHJpYnV0ZXMubGVuZ3RoOyBpKyspIHtcbiAgICB2YXIgYXR0ciA9ICRzcGFuLmF0dHJpYnV0ZXMuaXRlbShpKTtcbiAgICAkYnV0dG9uLnNldEF0dHJpYnV0ZShhdHRyLm5vZGVOYW1lLCBhdHRyLm5vZGVWYWx1ZSk7XG4gIH1cblxuICAkYnV0dG9uLmFkZEV2ZW50TGlzdGVuZXIoJ2ZvY3VzaW4nLCBmdW5jdGlvbiAoZSkge1xuICAgIGlmICghJGhlYWRlcldyYXBwZXIuY2xhc3NMaXN0LmNvbnRhaW5zKCRtb2R1bGUuc2VjdGlvbkhlYWRlckZvY3VzZWRDbGFzcykpIHtcbiAgICAgICRoZWFkZXJXcmFwcGVyLmNsYXNzTmFtZSArPSAnICcgKyAkbW9kdWxlLnNlY3Rpb25IZWFkZXJGb2N1c2VkQ2xhc3M7XG4gICAgfVxuICB9KTtcblxuICAkYnV0dG9uLmFkZEV2ZW50TGlzdGVuZXIoJ2JsdXInLCBmdW5jdGlvbiAoZSkge1xuICAgICRoZWFkZXJXcmFwcGVyLmNsYXNzTGlzdC5yZW1vdmUoJG1vZHVsZS5zZWN0aW9uSGVhZGVyRm9jdXNlZENsYXNzKTtcbiAgfSk7XG5cbiAgaWYgKHR5cGVvZiAoJHN1bW1hcnkpICE9PSAndW5kZWZpbmVkJyAmJiAkc3VtbWFyeSAhPT0gbnVsbCkge1xuICAgICRidXR0b24uc2V0QXR0cmlidXRlKCdhcmlhLWRlc2NyaWJlZGJ5JywgdGhpcy5tb2R1bGVJZCArICctc3VtbWFyeS0nICsgKGluZGV4ICsgMSkpO1xuICB9XG5cbiAgLy8gJHNwYW4gY291bGQgY29udGFpbiBIVE1MIGVsZW1lbnRzIChzZWUgaHR0cHM6Ly93d3cudzMub3JnL1RSLzIwMTEvV0QtaHRtbDUtMjAxMTA1MjUvY29udGVudC1tb2RlbHMuaHRtbCNwaHJhc2luZy1jb250ZW50KVxuICAkYnV0dG9uLmlubmVySFRNTCA9ICRzcGFuLmlubmVySFRNTDtcblxuICAkaGVhZGluZy5yZW1vdmVDaGlsZCgkc3Bhbik7XG4gICRoZWFkaW5nLmFwcGVuZENoaWxkKCRidXR0b24pO1xuXG4gIC8vIEFkZCBcIisvLVwiIGljb25cbiAgdmFyIGljb24gPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdzcGFuJyk7XG4gIGljb24uY2xhc3NOYW1lID0gdGhpcy5pY29uQ2xhc3M7XG4gIGljb24uc2V0QXR0cmlidXRlKCdhcmlhLWhpZGRlbicsICd0cnVlJyk7XG5cbiAgJGhlYWRpbmcuYXBwZW5kQ2hpbGQoaWNvbik7XG59O1xuXG4vLyBXaGVuIHNlY3Rpb24gdG9nZ2xlZCwgc2V0IGFuZCBzdG9yZSBzdGF0ZVxuQWNjb3JkaW9uLnByb3RvdHlwZS5vblNlY3Rpb25Ub2dnbGUgPSBmdW5jdGlvbiAoJHNlY3Rpb24pIHtcbiAgdmFyIGV4cGFuZGVkID0gdGhpcy5pc0V4cGFuZGVkKCRzZWN0aW9uKTtcbiAgdGhpcy5zZXRFeHBhbmRlZCghZXhwYW5kZWQsICRzZWN0aW9uKTtcblxuICAvLyBTdG9yZSB0aGUgc3RhdGUgaW4gc2Vzc2lvblN0b3JhZ2Ugd2hlbiBhIGNoYW5nZSBpcyB0cmlnZ2VyZWRcbiAgdGhpcy5zdG9yZVN0YXRlKCRzZWN0aW9uKTtcbn07XG5cbi8vIFdoZW4gT3Blbi9DbG9zZSBBbGwgdG9nZ2xlZCwgc2V0IGFuZCBzdG9yZSBzdGF0ZVxuQWNjb3JkaW9uLnByb3RvdHlwZS5vbk9wZW5PckNsb3NlQWxsVG9nZ2xlID0gZnVuY3Rpb24gKCkge1xuICB2YXIgJG1vZHVsZSA9IHRoaXM7XG4gIHZhciAkc2VjdGlvbnMgPSB0aGlzLiRzZWN0aW9ucztcblxuICB2YXIgbm93RXhwYW5kZWQgPSAhdGhpcy5jaGVja0lmQWxsU2VjdGlvbnNPcGVuKCk7XG5cbiAgbm9kZUxpc3RGb3JFYWNoKCRzZWN0aW9ucywgZnVuY3Rpb24gKCRzZWN0aW9uKSB7XG4gICAgJG1vZHVsZS5zZXRFeHBhbmRlZChub3dFeHBhbmRlZCwgJHNlY3Rpb24pO1xuICAgIC8vIFN0b3JlIHRoZSBzdGF0ZSBpbiBzZXNzaW9uU3RvcmFnZSB3aGVuIGEgY2hhbmdlIGlzIHRyaWdnZXJlZFxuICAgICRtb2R1bGUuc3RvcmVTdGF0ZSgkc2VjdGlvbik7XG4gIH0pO1xuXG4gICRtb2R1bGUudXBkYXRlT3BlbkFsbEJ1dHRvbihub3dFeHBhbmRlZCk7XG59O1xuXG4vLyBTZXQgc2VjdGlvbiBhdHRyaWJ1dGVzIHdoZW4gb3BlbmVkL2Nsb3NlZFxuQWNjb3JkaW9uLnByb3RvdHlwZS5zZXRFeHBhbmRlZCA9IGZ1bmN0aW9uIChleHBhbmRlZCwgJHNlY3Rpb24pIHtcbiAgdmFyICRidXR0b24gPSAkc2VjdGlvbi5xdWVyeVNlbGVjdG9yKCcuJyArIHRoaXMuc2VjdGlvbkJ1dHRvbkNsYXNzKTtcbiAgJGJ1dHRvbi5zZXRBdHRyaWJ1dGUoJ2FyaWEtZXhwYW5kZWQnLCBleHBhbmRlZCk7XG5cbiAgaWYgKGV4cGFuZGVkKSB7XG4gICAgJHNlY3Rpb24uY2xhc3NMaXN0LmFkZCh0aGlzLnNlY3Rpb25FeHBhbmRlZENsYXNzKTtcbiAgfSBlbHNlIHtcbiAgICAkc2VjdGlvbi5jbGFzc0xpc3QucmVtb3ZlKHRoaXMuc2VjdGlvbkV4cGFuZGVkQ2xhc3MpO1xuICB9XG5cbiAgLy8gU2VlIGlmIFwiT3BlbiBhbGxcIiBidXR0b24gdGV4dCBzaG91bGQgYmUgdXBkYXRlZFxuICB2YXIgYXJlQWxsU2VjdGlvbnNPcGVuID0gdGhpcy5jaGVja0lmQWxsU2VjdGlvbnNPcGVuKCk7XG4gIHRoaXMudXBkYXRlT3BlbkFsbEJ1dHRvbihhcmVBbGxTZWN0aW9uc09wZW4pO1xufTtcblxuLy8gR2V0IHN0YXRlIG9mIHNlY3Rpb25cbkFjY29yZGlvbi5wcm90b3R5cGUuaXNFeHBhbmRlZCA9IGZ1bmN0aW9uICgkc2VjdGlvbikge1xuICByZXR1cm4gJHNlY3Rpb24uY2xhc3NMaXN0LmNvbnRhaW5zKHRoaXMuc2VjdGlvbkV4cGFuZGVkQ2xhc3MpXG59O1xuXG4vLyBDaGVjayBpZiBhbGwgc2VjdGlvbnMgYXJlIG9wZW5cbkFjY29yZGlvbi5wcm90b3R5cGUuY2hlY2tJZkFsbFNlY3Rpb25zT3BlbiA9IGZ1bmN0aW9uICgpIHtcbiAgLy8gR2V0IGEgY291bnQgb2YgYWxsIHRoZSBBY2NvcmRpb24gc2VjdGlvbnNcbiAgdmFyIHNlY3Rpb25zQ291bnQgPSB0aGlzLiRzZWN0aW9ucy5sZW5ndGg7XG4gIC8vIEdldCBhIGNvdW50IG9mIGFsbCBBY2NvcmRpb24gc2VjdGlvbnMgdGhhdCBhcmUgZXhwYW5kZWRcbiAgdmFyIGV4cGFuZGVkU2VjdGlvbkNvdW50ID0gdGhpcy4kbW9kdWxlLnF1ZXJ5U2VsZWN0b3JBbGwoJy4nICsgdGhpcy5zZWN0aW9uRXhwYW5kZWRDbGFzcykubGVuZ3RoO1xuICB2YXIgYXJlQWxsU2VjdGlvbnNPcGVuID0gc2VjdGlvbnNDb3VudCA9PT0gZXhwYW5kZWRTZWN0aW9uQ291bnQ7XG5cbiAgcmV0dXJuIGFyZUFsbFNlY3Rpb25zT3BlblxufTtcblxuLy8gVXBkYXRlIFwiT3BlbiBhbGxcIiBidXR0b25cbkFjY29yZGlvbi5wcm90b3R5cGUudXBkYXRlT3BlbkFsbEJ1dHRvbiA9IGZ1bmN0aW9uIChleHBhbmRlZCkge1xuICB2YXIgbmV3QnV0dG9uVGV4dCA9IGV4cGFuZGVkID8gJ0Nsb3NlIGFsbCcgOiAnT3BlbiBhbGwnO1xuICBuZXdCdXR0b25UZXh0ICs9ICc8c3BhbiBjbGFzcz1cImdvdnVrLXZpc3VhbGx5LWhpZGRlblwiPiBzZWN0aW9uczwvc3Bhbj4nO1xuICB0aGlzLiRvcGVuQWxsQnV0dG9uLnNldEF0dHJpYnV0ZSgnYXJpYS1leHBhbmRlZCcsIGV4cGFuZGVkKTtcbiAgdGhpcy4kb3BlbkFsbEJ1dHRvbi5pbm5lckhUTUwgPSBuZXdCdXR0b25UZXh0O1xufTtcblxuLy8gQ2hlY2sgZm9yIGB3aW5kb3cuc2Vzc2lvblN0b3JhZ2VgLCBhbmQgdGhhdCBpdCBhY3R1YWxseSB3b3Jrcy5cbnZhciBoZWxwZXIgPSB7XG4gIGNoZWNrRm9yU2Vzc2lvblN0b3JhZ2U6IGZ1bmN0aW9uICgpIHtcbiAgICB2YXIgdGVzdFN0cmluZyA9ICd0aGlzIGlzIHRoZSB0ZXN0IHN0cmluZyc7XG4gICAgdmFyIHJlc3VsdDtcbiAgICB0cnkge1xuICAgICAgd2luZG93LnNlc3Npb25TdG9yYWdlLnNldEl0ZW0odGVzdFN0cmluZywgdGVzdFN0cmluZyk7XG4gICAgICByZXN1bHQgPSB3aW5kb3cuc2Vzc2lvblN0b3JhZ2UuZ2V0SXRlbSh0ZXN0U3RyaW5nKSA9PT0gdGVzdFN0cmluZy50b1N0cmluZygpO1xuICAgICAgd2luZG93LnNlc3Npb25TdG9yYWdlLnJlbW92ZUl0ZW0odGVzdFN0cmluZyk7XG4gICAgICByZXR1cm4gcmVzdWx0XG4gICAgfSBjYXRjaCAoZXhjZXB0aW9uKSB7XG4gICAgICBpZiAoKHR5cGVvZiBjb25zb2xlID09PSAndW5kZWZpbmVkJyB8fCB0eXBlb2YgY29uc29sZS5sb2cgPT09ICd1bmRlZmluZWQnKSkge1xuICAgICAgICBjb25zb2xlLmxvZygnTm90aWNlOiBzZXNzaW9uU3RvcmFnZSBub3QgYXZhaWxhYmxlLicpO1xuICAgICAgfVxuICAgIH1cbiAgfVxufTtcblxuLy8gU2V0IHRoZSBzdGF0ZSBvZiB0aGUgYWNjb3JkaW9ucyBpbiBzZXNzaW9uU3RvcmFnZVxuQWNjb3JkaW9uLnByb3RvdHlwZS5zdG9yZVN0YXRlID0gZnVuY3Rpb24gKCRzZWN0aW9uKSB7XG4gIGlmICh0aGlzLmJyb3dzZXJTdXBwb3J0c1Nlc3Npb25TdG9yYWdlKSB7XG4gICAgLy8gV2UgbmVlZCBhIHVuaXF1ZSB3YXkgb2YgaWRlbnRpZnlpbmcgZWFjaCBjb250ZW50IGluIHRoZSBhY2NvcmRpb24uIFNpbmNlXG4gICAgLy8gYW4gYCNpZGAgc2hvdWxkIGJlIHVuaXF1ZSBhbmQgYW4gYGlkYCBpcyByZXF1aXJlZCBmb3IgYGFyaWEtYCBhdHRyaWJ1dGVzXG4gICAgLy8gYGlkYCBjYW4gYmUgc2FmZWx5IHVzZWQuXG4gICAgdmFyICRidXR0b24gPSAkc2VjdGlvbi5xdWVyeVNlbGVjdG9yKCcuJyArIHRoaXMuc2VjdGlvbkJ1dHRvbkNsYXNzKTtcblxuICAgIGlmICgkYnV0dG9uKSB7XG4gICAgICB2YXIgY29udGVudElkID0gJGJ1dHRvbi5nZXRBdHRyaWJ1dGUoJ2FyaWEtY29udHJvbHMnKTtcbiAgICAgIHZhciBjb250ZW50U3RhdGUgPSAkYnV0dG9uLmdldEF0dHJpYnV0ZSgnYXJpYS1leHBhbmRlZCcpO1xuXG4gICAgICBpZiAodHlwZW9mIGNvbnRlbnRJZCA9PT0gJ3VuZGVmaW5lZCcgJiYgKHR5cGVvZiBjb25zb2xlID09PSAndW5kZWZpbmVkJyB8fCB0eXBlb2YgY29uc29sZS5sb2cgPT09ICd1bmRlZmluZWQnKSkge1xuICAgICAgICBjb25zb2xlLmVycm9yKG5ldyBFcnJvcignTm8gYXJpYSBjb250cm9scyBwcmVzZW50IGluIGFjY29yZGlvbiBzZWN0aW9uIGhlYWRpbmcuJykpO1xuICAgICAgfVxuXG4gICAgICBpZiAodHlwZW9mIGNvbnRlbnRTdGF0ZSA9PT0gJ3VuZGVmaW5lZCcgJiYgKHR5cGVvZiBjb25zb2xlID09PSAndW5kZWZpbmVkJyB8fCB0eXBlb2YgY29uc29sZS5sb2cgPT09ICd1bmRlZmluZWQnKSkge1xuICAgICAgICBjb25zb2xlLmVycm9yKG5ldyBFcnJvcignTm8gYXJpYSBleHBhbmRlZCBwcmVzZW50IGluIGFjY29yZGlvbiBzZWN0aW9uIGhlYWRpbmcuJykpO1xuICAgICAgfVxuXG4gICAgICAvLyBPbmx5IHNldCB0aGUgc3RhdGUgd2hlbiBib3RoIGBjb250ZW50SWRgIGFuZCBgY29udGVudFN0YXRlYCBhcmUgdGFrZW4gZnJvbSB0aGUgRE9NLlxuICAgICAgaWYgKGNvbnRlbnRJZCAmJiBjb250ZW50U3RhdGUpIHtcbiAgICAgICAgd2luZG93LnNlc3Npb25TdG9yYWdlLnNldEl0ZW0oY29udGVudElkLCBjb250ZW50U3RhdGUpO1xuICAgICAgfVxuICAgIH1cbiAgfVxufTtcblxuLy8gUmVhZCB0aGUgc3RhdGUgb2YgdGhlIGFjY29yZGlvbnMgZnJvbSBzZXNzaW9uU3RvcmFnZVxuQWNjb3JkaW9uLnByb3RvdHlwZS5zZXRJbml0aWFsU3RhdGUgPSBmdW5jdGlvbiAoJHNlY3Rpb24pIHtcbiAgaWYgKHRoaXMuYnJvd3NlclN1cHBvcnRzU2Vzc2lvblN0b3JhZ2UpIHtcbiAgICB2YXIgJGJ1dHRvbiA9ICRzZWN0aW9uLnF1ZXJ5U2VsZWN0b3IoJy4nICsgdGhpcy5zZWN0aW9uQnV0dG9uQ2xhc3MpO1xuXG4gICAgaWYgKCRidXR0b24pIHtcbiAgICAgIHZhciBjb250ZW50SWQgPSAkYnV0dG9uLmdldEF0dHJpYnV0ZSgnYXJpYS1jb250cm9scycpO1xuICAgICAgdmFyIGNvbnRlbnRTdGF0ZSA9IGNvbnRlbnRJZCA/IHdpbmRvdy5zZXNzaW9uU3RvcmFnZS5nZXRJdGVtKGNvbnRlbnRJZCkgOiBudWxsO1xuXG4gICAgICBpZiAoY29udGVudFN0YXRlICE9PSBudWxsKSB7XG4gICAgICAgIHRoaXMuc2V0RXhwYW5kZWQoY29udGVudFN0YXRlID09PSAndHJ1ZScsICRzZWN0aW9uKTtcbiAgICAgIH1cbiAgICB9XG4gIH1cbn07XG5cbihmdW5jdGlvbih1bmRlZmluZWQpIHtcblxuLy8gRGV0ZWN0aW9uIGZyb20gaHR0cHM6Ly9naXRodWIuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlL2Jsb2IvbWFzdGVyL3BhY2thZ2VzL3BvbHlmaWxsLWxpYnJhcnkvcG9seWZpbGxzL1dpbmRvdy9kZXRlY3QuanNcbnZhciBkZXRlY3QgPSAoJ1dpbmRvdycgaW4gdGhpcyk7XG5cbmlmIChkZXRlY3QpIHJldHVyblxuXG4vLyBQb2x5ZmlsbCBmcm9tIGh0dHBzOi8vY2RuLnBvbHlmaWxsLmlvL3YyL3BvbHlmaWxsLmpzP2ZlYXR1cmVzPVdpbmRvdyZmbGFncz1hbHdheXNcbmlmICgodHlwZW9mIFdvcmtlckdsb2JhbFNjb3BlID09PSBcInVuZGVmaW5lZFwiKSAmJiAodHlwZW9mIGltcG9ydFNjcmlwdHMgIT09IFwiZnVuY3Rpb25cIikpIHtcblx0KGZ1bmN0aW9uIChnbG9iYWwpIHtcblx0XHRpZiAoZ2xvYmFsLmNvbnN0cnVjdG9yKSB7XG5cdFx0XHRnbG9iYWwuV2luZG93ID0gZ2xvYmFsLmNvbnN0cnVjdG9yO1xuXHRcdH0gZWxzZSB7XG5cdFx0XHQoZ2xvYmFsLldpbmRvdyA9IGdsb2JhbC5jb25zdHJ1Y3RvciA9IG5ldyBGdW5jdGlvbigncmV0dXJuIGZ1bmN0aW9uIFdpbmRvdygpIHt9JykoKSkucHJvdG90eXBlID0gdGhpcztcblx0XHR9XG5cdH0odGhpcykpO1xufVxuXG59KVxuLmNhbGwoJ29iamVjdCcgPT09IHR5cGVvZiB3aW5kb3cgJiYgd2luZG93IHx8ICdvYmplY3QnID09PSB0eXBlb2Ygc2VsZiAmJiBzZWxmIHx8ICdvYmplY3QnID09PSB0eXBlb2YgZ2xvYmFsICYmIGdsb2JhbCB8fCB7fSk7XG5cbihmdW5jdGlvbih1bmRlZmluZWQpIHtcblxuLy8gRGV0ZWN0aW9uIGZyb20gaHR0cHM6Ly9naXRodWIuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlL2Jsb2IvbWFzdGVyL3BhY2thZ2VzL3BvbHlmaWxsLWxpYnJhcnkvcG9seWZpbGxzL0V2ZW50L2RldGVjdC5qc1xudmFyIGRldGVjdCA9IChcbiAgKGZ1bmN0aW9uKGdsb2JhbCkge1xuXG4gIFx0aWYgKCEoJ0V2ZW50JyBpbiBnbG9iYWwpKSByZXR1cm4gZmFsc2U7XG4gIFx0aWYgKHR5cGVvZiBnbG9iYWwuRXZlbnQgPT09ICdmdW5jdGlvbicpIHJldHVybiB0cnVlO1xuXG4gIFx0dHJ5IHtcblxuICBcdFx0Ly8gSW4gSUUgOS0xMSwgdGhlIEV2ZW50IG9iamVjdCBleGlzdHMgYnV0IGNhbm5vdCBiZSBpbnN0YW50aWF0ZWRcbiAgXHRcdG5ldyBFdmVudCgnY2xpY2snKTtcbiAgXHRcdHJldHVybiB0cnVlO1xuICBcdH0gY2F0Y2goZSkge1xuICBcdFx0cmV0dXJuIGZhbHNlO1xuICBcdH1cbiAgfSh0aGlzKSlcbik7XG5cbmlmIChkZXRlY3QpIHJldHVyblxuXG4vLyBQb2x5ZmlsbCBmcm9tIGh0dHBzOi8vY2RuLnBvbHlmaWxsLmlvL3YyL3BvbHlmaWxsLmpzP2ZlYXR1cmVzPUV2ZW50JmZsYWdzPWFsd2F5c1xuKGZ1bmN0aW9uICgpIHtcblx0dmFyIHVubGlzdGVuYWJsZVdpbmRvd0V2ZW50cyA9IHtcblx0XHRjbGljazogMSxcblx0XHRkYmxjbGljazogMSxcblx0XHRrZXl1cDogMSxcblx0XHRrZXlwcmVzczogMSxcblx0XHRrZXlkb3duOiAxLFxuXHRcdG1vdXNlZG93bjogMSxcblx0XHRtb3VzZXVwOiAxLFxuXHRcdG1vdXNlbW92ZTogMSxcblx0XHRtb3VzZW92ZXI6IDEsXG5cdFx0bW91c2VlbnRlcjogMSxcblx0XHRtb3VzZWxlYXZlOiAxLFxuXHRcdG1vdXNlb3V0OiAxLFxuXHRcdHN0b3JhZ2U6IDEsXG5cdFx0c3RvcmFnZWNvbW1pdDogMSxcblx0XHR0ZXh0aW5wdXQ6IDFcblx0fTtcblxuXHQvLyBUaGlzIHBvbHlmaWxsIGRlcGVuZHMgb24gYXZhaWxhYmlsaXR5IG9mIGBkb2N1bWVudGAgc28gd2lsbCBub3QgcnVuIGluIGEgd29ya2VyXG5cdC8vIEhvd2V2ZXIsIHdlIGFzc3N1bWUgdGhlcmUgYXJlIG5vIGJyb3dzZXJzIHdpdGggd29ya2VyIHN1cHBvcnQgdGhhdCBsYWNrIHByb3BlclxuXHQvLyBzdXBwb3J0IGZvciBgRXZlbnRgIHdpdGhpbiB0aGUgd29ya2VyXG5cdGlmICh0eXBlb2YgZG9jdW1lbnQgPT09ICd1bmRlZmluZWQnIHx8IHR5cGVvZiB3aW5kb3cgPT09ICd1bmRlZmluZWQnKSByZXR1cm47XG5cblx0ZnVuY3Rpb24gaW5kZXhPZihhcnJheSwgZWxlbWVudCkge1xuXHRcdHZhclxuXHRcdGluZGV4ID0gLTEsXG5cdFx0bGVuZ3RoID0gYXJyYXkubGVuZ3RoO1xuXG5cdFx0d2hpbGUgKCsraW5kZXggPCBsZW5ndGgpIHtcblx0XHRcdGlmIChpbmRleCBpbiBhcnJheSAmJiBhcnJheVtpbmRleF0gPT09IGVsZW1lbnQpIHtcblx0XHRcdFx0cmV0dXJuIGluZGV4O1xuXHRcdFx0fVxuXHRcdH1cblxuXHRcdHJldHVybiAtMTtcblx0fVxuXG5cdHZhciBleGlzdGluZ1Byb3RvID0gKHdpbmRvdy5FdmVudCAmJiB3aW5kb3cuRXZlbnQucHJvdG90eXBlKSB8fCBudWxsO1xuXHR3aW5kb3cuRXZlbnQgPSBXaW5kb3cucHJvdG90eXBlLkV2ZW50ID0gZnVuY3Rpb24gRXZlbnQodHlwZSwgZXZlbnRJbml0RGljdCkge1xuXHRcdGlmICghdHlwZSkge1xuXHRcdFx0dGhyb3cgbmV3IEVycm9yKCdOb3QgZW5vdWdoIGFyZ3VtZW50cycpO1xuXHRcdH1cblxuXHRcdHZhciBldmVudDtcblx0XHQvLyBTaG9ydGN1dCBpZiBicm93c2VyIHN1cHBvcnRzIGNyZWF0ZUV2ZW50XG5cdFx0aWYgKCdjcmVhdGVFdmVudCcgaW4gZG9jdW1lbnQpIHtcblx0XHRcdGV2ZW50ID0gZG9jdW1lbnQuY3JlYXRlRXZlbnQoJ0V2ZW50Jyk7XG5cdFx0XHR2YXIgYnViYmxlcyA9IGV2ZW50SW5pdERpY3QgJiYgZXZlbnRJbml0RGljdC5idWJibGVzICE9PSB1bmRlZmluZWQgPyBldmVudEluaXREaWN0LmJ1YmJsZXMgOiBmYWxzZTtcblx0XHRcdHZhciBjYW5jZWxhYmxlID0gZXZlbnRJbml0RGljdCAmJiBldmVudEluaXREaWN0LmNhbmNlbGFibGUgIT09IHVuZGVmaW5lZCA/IGV2ZW50SW5pdERpY3QuY2FuY2VsYWJsZSA6IGZhbHNlO1xuXG5cdFx0XHRldmVudC5pbml0RXZlbnQodHlwZSwgYnViYmxlcywgY2FuY2VsYWJsZSk7XG5cblx0XHRcdHJldHVybiBldmVudDtcblx0XHR9XG5cblx0XHRldmVudCA9IGRvY3VtZW50LmNyZWF0ZUV2ZW50T2JqZWN0KCk7XG5cblx0XHRldmVudC50eXBlID0gdHlwZTtcblx0XHRldmVudC5idWJibGVzID0gZXZlbnRJbml0RGljdCAmJiBldmVudEluaXREaWN0LmJ1YmJsZXMgIT09IHVuZGVmaW5lZCA/IGV2ZW50SW5pdERpY3QuYnViYmxlcyA6IGZhbHNlO1xuXHRcdGV2ZW50LmNhbmNlbGFibGUgPSBldmVudEluaXREaWN0ICYmIGV2ZW50SW5pdERpY3QuY2FuY2VsYWJsZSAhPT0gdW5kZWZpbmVkID8gZXZlbnRJbml0RGljdC5jYW5jZWxhYmxlIDogZmFsc2U7XG5cblx0XHRyZXR1cm4gZXZlbnQ7XG5cdH07XG5cdGlmIChleGlzdGluZ1Byb3RvKSB7XG5cdFx0T2JqZWN0LmRlZmluZVByb3BlcnR5KHdpbmRvdy5FdmVudCwgJ3Byb3RvdHlwZScsIHtcblx0XHRcdGNvbmZpZ3VyYWJsZTogZmFsc2UsXG5cdFx0XHRlbnVtZXJhYmxlOiBmYWxzZSxcblx0XHRcdHdyaXRhYmxlOiB0cnVlLFxuXHRcdFx0dmFsdWU6IGV4aXN0aW5nUHJvdG9cblx0XHR9KTtcblx0fVxuXG5cdGlmICghKCdjcmVhdGVFdmVudCcgaW4gZG9jdW1lbnQpKSB7XG5cdFx0d2luZG93LmFkZEV2ZW50TGlzdGVuZXIgPSBXaW5kb3cucHJvdG90eXBlLmFkZEV2ZW50TGlzdGVuZXIgPSBEb2N1bWVudC5wcm90b3R5cGUuYWRkRXZlbnRMaXN0ZW5lciA9IEVsZW1lbnQucHJvdG90eXBlLmFkZEV2ZW50TGlzdGVuZXIgPSBmdW5jdGlvbiBhZGRFdmVudExpc3RlbmVyKCkge1xuXHRcdFx0dmFyXG5cdFx0XHRlbGVtZW50ID0gdGhpcyxcblx0XHRcdHR5cGUgPSBhcmd1bWVudHNbMF0sXG5cdFx0XHRsaXN0ZW5lciA9IGFyZ3VtZW50c1sxXTtcblxuXHRcdFx0aWYgKGVsZW1lbnQgPT09IHdpbmRvdyAmJiB0eXBlIGluIHVubGlzdGVuYWJsZVdpbmRvd0V2ZW50cykge1xuXHRcdFx0XHR0aHJvdyBuZXcgRXJyb3IoJ0luIElFOCB0aGUgZXZlbnQ6ICcgKyB0eXBlICsgJyBpcyBub3QgYXZhaWxhYmxlIG9uIHRoZSB3aW5kb3cgb2JqZWN0LiBQbGVhc2Ugc2VlIGh0dHBzOi8vZ2l0aHViLmNvbS9GaW5hbmNpYWwtVGltZXMvcG9seWZpbGwtc2VydmljZS9pc3N1ZXMvMzE3IGZvciBtb3JlIGluZm9ybWF0aW9uLicpO1xuXHRcdFx0fVxuXG5cdFx0XHRpZiAoIWVsZW1lbnQuX2V2ZW50cykge1xuXHRcdFx0XHRlbGVtZW50Ll9ldmVudHMgPSB7fTtcblx0XHRcdH1cblxuXHRcdFx0aWYgKCFlbGVtZW50Ll9ldmVudHNbdHlwZV0pIHtcblx0XHRcdFx0ZWxlbWVudC5fZXZlbnRzW3R5cGVdID0gZnVuY3Rpb24gKGV2ZW50KSB7XG5cdFx0XHRcdFx0dmFyXG5cdFx0XHRcdFx0bGlzdCA9IGVsZW1lbnQuX2V2ZW50c1tldmVudC50eXBlXS5saXN0LFxuXHRcdFx0XHRcdGV2ZW50cyA9IGxpc3Quc2xpY2UoKSxcblx0XHRcdFx0XHRpbmRleCA9IC0xLFxuXHRcdFx0XHRcdGxlbmd0aCA9IGV2ZW50cy5sZW5ndGgsXG5cdFx0XHRcdFx0ZXZlbnRFbGVtZW50O1xuXG5cdFx0XHRcdFx0ZXZlbnQucHJldmVudERlZmF1bHQgPSBmdW5jdGlvbiBwcmV2ZW50RGVmYXVsdCgpIHtcblx0XHRcdFx0XHRcdGlmIChldmVudC5jYW5jZWxhYmxlICE9PSBmYWxzZSkge1xuXHRcdFx0XHRcdFx0XHRldmVudC5yZXR1cm5WYWx1ZSA9IGZhbHNlO1xuXHRcdFx0XHRcdFx0fVxuXHRcdFx0XHRcdH07XG5cblx0XHRcdFx0XHRldmVudC5zdG9wUHJvcGFnYXRpb24gPSBmdW5jdGlvbiBzdG9wUHJvcGFnYXRpb24oKSB7XG5cdFx0XHRcdFx0XHRldmVudC5jYW5jZWxCdWJibGUgPSB0cnVlO1xuXHRcdFx0XHRcdH07XG5cblx0XHRcdFx0XHRldmVudC5zdG9wSW1tZWRpYXRlUHJvcGFnYXRpb24gPSBmdW5jdGlvbiBzdG9wSW1tZWRpYXRlUHJvcGFnYXRpb24oKSB7XG5cdFx0XHRcdFx0XHRldmVudC5jYW5jZWxCdWJibGUgPSB0cnVlO1xuXHRcdFx0XHRcdFx0ZXZlbnQuY2FuY2VsSW1tZWRpYXRlID0gdHJ1ZTtcblx0XHRcdFx0XHR9O1xuXG5cdFx0XHRcdFx0ZXZlbnQuY3VycmVudFRhcmdldCA9IGVsZW1lbnQ7XG5cdFx0XHRcdFx0ZXZlbnQucmVsYXRlZFRhcmdldCA9IGV2ZW50LmZyb21FbGVtZW50IHx8IG51bGw7XG5cdFx0XHRcdFx0ZXZlbnQudGFyZ2V0ID0gZXZlbnQudGFyZ2V0IHx8IGV2ZW50LnNyY0VsZW1lbnQgfHwgZWxlbWVudDtcblx0XHRcdFx0XHRldmVudC50aW1lU3RhbXAgPSBuZXcgRGF0ZSgpLmdldFRpbWUoKTtcblxuXHRcdFx0XHRcdGlmIChldmVudC5jbGllbnRYKSB7XG5cdFx0XHRcdFx0XHRldmVudC5wYWdlWCA9IGV2ZW50LmNsaWVudFggKyBkb2N1bWVudC5kb2N1bWVudEVsZW1lbnQuc2Nyb2xsTGVmdDtcblx0XHRcdFx0XHRcdGV2ZW50LnBhZ2VZID0gZXZlbnQuY2xpZW50WSArIGRvY3VtZW50LmRvY3VtZW50RWxlbWVudC5zY3JvbGxUb3A7XG5cdFx0XHRcdFx0fVxuXG5cdFx0XHRcdFx0d2hpbGUgKCsraW5kZXggPCBsZW5ndGggJiYgIWV2ZW50LmNhbmNlbEltbWVkaWF0ZSkge1xuXHRcdFx0XHRcdFx0aWYgKGluZGV4IGluIGV2ZW50cykge1xuXHRcdFx0XHRcdFx0XHRldmVudEVsZW1lbnQgPSBldmVudHNbaW5kZXhdO1xuXG5cdFx0XHRcdFx0XHRcdGlmIChpbmRleE9mKGxpc3QsIGV2ZW50RWxlbWVudCkgIT09IC0xICYmIHR5cGVvZiBldmVudEVsZW1lbnQgPT09ICdmdW5jdGlvbicpIHtcblx0XHRcdFx0XHRcdFx0XHRldmVudEVsZW1lbnQuY2FsbChlbGVtZW50LCBldmVudCk7XG5cdFx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHR9XG5cdFx0XHRcdH07XG5cblx0XHRcdFx0ZWxlbWVudC5fZXZlbnRzW3R5cGVdLmxpc3QgPSBbXTtcblxuXHRcdFx0XHRpZiAoZWxlbWVudC5hdHRhY2hFdmVudCkge1xuXHRcdFx0XHRcdGVsZW1lbnQuYXR0YWNoRXZlbnQoJ29uJyArIHR5cGUsIGVsZW1lbnQuX2V2ZW50c1t0eXBlXSk7XG5cdFx0XHRcdH1cblx0XHRcdH1cblxuXHRcdFx0ZWxlbWVudC5fZXZlbnRzW3R5cGVdLmxpc3QucHVzaChsaXN0ZW5lcik7XG5cdFx0fTtcblxuXHRcdHdpbmRvdy5yZW1vdmVFdmVudExpc3RlbmVyID0gV2luZG93LnByb3RvdHlwZS5yZW1vdmVFdmVudExpc3RlbmVyID0gRG9jdW1lbnQucHJvdG90eXBlLnJlbW92ZUV2ZW50TGlzdGVuZXIgPSBFbGVtZW50LnByb3RvdHlwZS5yZW1vdmVFdmVudExpc3RlbmVyID0gZnVuY3Rpb24gcmVtb3ZlRXZlbnRMaXN0ZW5lcigpIHtcblx0XHRcdHZhclxuXHRcdFx0ZWxlbWVudCA9IHRoaXMsXG5cdFx0XHR0eXBlID0gYXJndW1lbnRzWzBdLFxuXHRcdFx0bGlzdGVuZXIgPSBhcmd1bWVudHNbMV0sXG5cdFx0XHRpbmRleDtcblxuXHRcdFx0aWYgKGVsZW1lbnQuX2V2ZW50cyAmJiBlbGVtZW50Ll9ldmVudHNbdHlwZV0gJiYgZWxlbWVudC5fZXZlbnRzW3R5cGVdLmxpc3QpIHtcblx0XHRcdFx0aW5kZXggPSBpbmRleE9mKGVsZW1lbnQuX2V2ZW50c1t0eXBlXS5saXN0LCBsaXN0ZW5lcik7XG5cblx0XHRcdFx0aWYgKGluZGV4ICE9PSAtMSkge1xuXHRcdFx0XHRcdGVsZW1lbnQuX2V2ZW50c1t0eXBlXS5saXN0LnNwbGljZShpbmRleCwgMSk7XG5cblx0XHRcdFx0XHRpZiAoIWVsZW1lbnQuX2V2ZW50c1t0eXBlXS5saXN0Lmxlbmd0aCkge1xuXHRcdFx0XHRcdFx0aWYgKGVsZW1lbnQuZGV0YWNoRXZlbnQpIHtcblx0XHRcdFx0XHRcdFx0ZWxlbWVudC5kZXRhY2hFdmVudCgnb24nICsgdHlwZSwgZWxlbWVudC5fZXZlbnRzW3R5cGVdKTtcblx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHRcdGRlbGV0ZSBlbGVtZW50Ll9ldmVudHNbdHlwZV07XG5cdFx0XHRcdFx0fVxuXHRcdFx0XHR9XG5cdFx0XHR9XG5cdFx0fTtcblxuXHRcdHdpbmRvdy5kaXNwYXRjaEV2ZW50ID0gV2luZG93LnByb3RvdHlwZS5kaXNwYXRjaEV2ZW50ID0gRG9jdW1lbnQucHJvdG90eXBlLmRpc3BhdGNoRXZlbnQgPSBFbGVtZW50LnByb3RvdHlwZS5kaXNwYXRjaEV2ZW50ID0gZnVuY3Rpb24gZGlzcGF0Y2hFdmVudChldmVudCkge1xuXHRcdFx0aWYgKCFhcmd1bWVudHMubGVuZ3RoKSB7XG5cdFx0XHRcdHRocm93IG5ldyBFcnJvcignTm90IGVub3VnaCBhcmd1bWVudHMnKTtcblx0XHRcdH1cblxuXHRcdFx0aWYgKCFldmVudCB8fCB0eXBlb2YgZXZlbnQudHlwZSAhPT0gJ3N0cmluZycpIHtcblx0XHRcdFx0dGhyb3cgbmV3IEVycm9yKCdET00gRXZlbnRzIEV4Y2VwdGlvbiAwJyk7XG5cdFx0XHR9XG5cblx0XHRcdHZhciBlbGVtZW50ID0gdGhpcywgdHlwZSA9IGV2ZW50LnR5cGU7XG5cblx0XHRcdHRyeSB7XG5cdFx0XHRcdGlmICghZXZlbnQuYnViYmxlcykge1xuXHRcdFx0XHRcdGV2ZW50LmNhbmNlbEJ1YmJsZSA9IHRydWU7XG5cblx0XHRcdFx0XHR2YXIgY2FuY2VsQnViYmxlRXZlbnQgPSBmdW5jdGlvbiAoZXZlbnQpIHtcblx0XHRcdFx0XHRcdGV2ZW50LmNhbmNlbEJ1YmJsZSA9IHRydWU7XG5cblx0XHRcdFx0XHRcdChlbGVtZW50IHx8IHdpbmRvdykuZGV0YWNoRXZlbnQoJ29uJyArIHR5cGUsIGNhbmNlbEJ1YmJsZUV2ZW50KTtcblx0XHRcdFx0XHR9O1xuXG5cdFx0XHRcdFx0dGhpcy5hdHRhY2hFdmVudCgnb24nICsgdHlwZSwgY2FuY2VsQnViYmxlRXZlbnQpO1xuXHRcdFx0XHR9XG5cblx0XHRcdFx0dGhpcy5maXJlRXZlbnQoJ29uJyArIHR5cGUsIGV2ZW50KTtcblx0XHRcdH0gY2F0Y2ggKGVycm9yKSB7XG5cdFx0XHRcdGV2ZW50LnRhcmdldCA9IGVsZW1lbnQ7XG5cblx0XHRcdFx0ZG8ge1xuXHRcdFx0XHRcdGV2ZW50LmN1cnJlbnRUYXJnZXQgPSBlbGVtZW50O1xuXG5cdFx0XHRcdFx0aWYgKCdfZXZlbnRzJyBpbiBlbGVtZW50ICYmIHR5cGVvZiBlbGVtZW50Ll9ldmVudHNbdHlwZV0gPT09ICdmdW5jdGlvbicpIHtcblx0XHRcdFx0XHRcdGVsZW1lbnQuX2V2ZW50c1t0eXBlXS5jYWxsKGVsZW1lbnQsIGV2ZW50KTtcblx0XHRcdFx0XHR9XG5cblx0XHRcdFx0XHRpZiAodHlwZW9mIGVsZW1lbnRbJ29uJyArIHR5cGVdID09PSAnZnVuY3Rpb24nKSB7XG5cdFx0XHRcdFx0XHRlbGVtZW50WydvbicgKyB0eXBlXS5jYWxsKGVsZW1lbnQsIGV2ZW50KTtcblx0XHRcdFx0XHR9XG5cblx0XHRcdFx0XHRlbGVtZW50ID0gZWxlbWVudC5ub2RlVHlwZSA9PT0gOSA/IGVsZW1lbnQucGFyZW50V2luZG93IDogZWxlbWVudC5wYXJlbnROb2RlO1xuXHRcdFx0XHR9IHdoaWxlIChlbGVtZW50ICYmICFldmVudC5jYW5jZWxCdWJibGUpO1xuXHRcdFx0fVxuXG5cdFx0XHRyZXR1cm4gdHJ1ZTtcblx0XHR9O1xuXG5cdFx0Ly8gQWRkIHRoZSBET01Db250ZW50TG9hZGVkIEV2ZW50XG5cdFx0ZG9jdW1lbnQuYXR0YWNoRXZlbnQoJ29ucmVhZHlzdGF0ZWNoYW5nZScsIGZ1bmN0aW9uKCkge1xuXHRcdFx0aWYgKGRvY3VtZW50LnJlYWR5U3RhdGUgPT09ICdjb21wbGV0ZScpIHtcblx0XHRcdFx0ZG9jdW1lbnQuZGlzcGF0Y2hFdmVudChuZXcgRXZlbnQoJ0RPTUNvbnRlbnRMb2FkZWQnLCB7XG5cdFx0XHRcdFx0YnViYmxlczogdHJ1ZVxuXHRcdFx0XHR9KSk7XG5cdFx0XHR9XG5cdFx0fSk7XG5cdH1cbn0oKSk7XG5cbn0pXG4uY2FsbCgnb2JqZWN0JyA9PT0gdHlwZW9mIHdpbmRvdyAmJiB3aW5kb3cgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBzZWxmICYmIHNlbGYgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBnbG9iYWwgJiYgZ2xvYmFsIHx8IHt9KTtcblxudmFyIEtFWV9TUEFDRSA9IDMyO1xudmFyIERFQk9VTkNFX1RJTUVPVVRfSU5fU0VDT05EUyA9IDE7XG5cbmZ1bmN0aW9uIEJ1dHRvbiAoJG1vZHVsZSkge1xuICB0aGlzLiRtb2R1bGUgPSAkbW9kdWxlO1xuICB0aGlzLmRlYm91bmNlRm9ybVN1Ym1pdFRpbWVyID0gbnVsbDtcbn1cblxuLyoqXG4qIEphdmFTY3JpcHQgJ3NoaW0nIHRvIHRyaWdnZXIgdGhlIGNsaWNrIGV2ZW50IG9mIGVsZW1lbnQocykgd2hlbiB0aGUgc3BhY2Uga2V5IGlzIHByZXNzZWQuXG4qXG4qIENyZWF0ZWQgc2luY2Ugc29tZSBBc3Npc3RpdmUgVGVjaG5vbG9naWVzIChmb3IgZXhhbXBsZSBzb21lIFNjcmVlbnJlYWRlcnMpXG4qIHdpbGwgdGVsbCBhIHVzZXIgdG8gcHJlc3Mgc3BhY2Ugb24gYSAnYnV0dG9uJywgc28gdGhpcyBmdW5jdGlvbmFsaXR5IG5lZWRzIHRvIGJlIHNoaW1tZWRcbiogU2VlIGh0dHBzOi8vZ2l0aHViLmNvbS9hbHBoYWdvdi9nb3Z1a19lbGVtZW50cy9wdWxsLzI3MiNpc3N1ZWNvbW1lbnQtMjMzMDI4MjcwXG4qXG4qIEBwYXJhbSB7b2JqZWN0fSBldmVudCBldmVudFxuKi9cbkJ1dHRvbi5wcm90b3R5cGUuaGFuZGxlS2V5RG93biA9IGZ1bmN0aW9uIChldmVudCkge1xuICAvLyBnZXQgdGhlIHRhcmdldCBlbGVtZW50XG4gIHZhciB0YXJnZXQgPSBldmVudC50YXJnZXQ7XG4gIC8vIGlmIHRoZSBlbGVtZW50IGhhcyBhIHJvbGU9J2J1dHRvbicgYW5kIHRoZSBwcmVzc2VkIGtleSBpcyBhIHNwYWNlLCB3ZSdsbCBzaW11bGF0ZSBhIGNsaWNrXG4gIGlmICh0YXJnZXQuZ2V0QXR0cmlidXRlKCdyb2xlJykgPT09ICdidXR0b24nICYmIGV2ZW50LmtleUNvZGUgPT09IEtFWV9TUEFDRSkge1xuICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XG4gICAgLy8gdHJpZ2dlciB0aGUgdGFyZ2V0J3MgY2xpY2sgZXZlbnRcbiAgICB0YXJnZXQuY2xpY2soKTtcbiAgfVxufTtcblxuLyoqXG4qIElmIHRoZSBjbGljayBxdWlja2x5IHN1Y2NlZWRzIGEgcHJldmlvdXMgY2xpY2sgdGhlbiBub3RoaW5nIHdpbGwgaGFwcGVuLlxuKiBUaGlzIHN0b3BzIHBlb3BsZSBhY2NpZGVudGFsbHkgY2F1c2luZyBtdWx0aXBsZSBmb3JtIHN1Ym1pc3Npb25zIGJ5XG4qIGRvdWJsZSBjbGlja2luZyBidXR0b25zLlxuKi9cbkJ1dHRvbi5wcm90b3R5cGUuZGVib3VuY2UgPSBmdW5jdGlvbiAoZXZlbnQpIHtcbiAgdmFyIHRhcmdldCA9IGV2ZW50LnRhcmdldDtcbiAgLy8gQ2hlY2sgdGhlIGJ1dHRvbiB0aGF0IGlzIGNsaWNrZWQgb24gaGFzIHRoZSBwcmV2ZW50RG91YmxlQ2xpY2sgZmVhdHVyZSBlbmFibGVkXG4gIGlmICh0YXJnZXQuZ2V0QXR0cmlidXRlKCdkYXRhLXByZXZlbnQtZG91YmxlLWNsaWNrJykgIT09ICd0cnVlJykge1xuICAgIHJldHVyblxuICB9XG5cbiAgLy8gSWYgdGhlIHRpbWVyIGlzIHN0aWxsIHJ1bm5pbmcgdGhlbiB3ZSB3YW50IHRvIHByZXZlbnQgdGhlIGNsaWNrIGZyb20gc3VibWl0dGluZyB0aGUgZm9ybVxuICBpZiAodGhpcy5kZWJvdW5jZUZvcm1TdWJtaXRUaW1lcikge1xuICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XG4gICAgcmV0dXJuIGZhbHNlXG4gIH1cblxuICB0aGlzLmRlYm91bmNlRm9ybVN1Ym1pdFRpbWVyID0gc2V0VGltZW91dChmdW5jdGlvbiAoKSB7XG4gICAgdGhpcy5kZWJvdW5jZUZvcm1TdWJtaXRUaW1lciA9IG51bGw7XG4gIH0uYmluZCh0aGlzKSwgREVCT1VOQ0VfVElNRU9VVF9JTl9TRUNPTkRTICogMTAwMCk7XG59O1xuXG4vKipcbiogSW5pdGlhbGlzZSBhbiBldmVudCBsaXN0ZW5lciBmb3Iga2V5ZG93biBhdCBkb2N1bWVudCBsZXZlbFxuKiB0aGlzIHdpbGwgaGVscCBsaXN0ZW5pbmcgZm9yIGxhdGVyIGluc2VydGVkIGVsZW1lbnRzIHdpdGggYSByb2xlPVwiYnV0dG9uXCJcbiovXG5CdXR0b24ucHJvdG90eXBlLmluaXQgPSBmdW5jdGlvbiAoKSB7XG4gIHRoaXMuJG1vZHVsZS5hZGRFdmVudExpc3RlbmVyKCdrZXlkb3duJywgdGhpcy5oYW5kbGVLZXlEb3duKTtcbiAgdGhpcy4kbW9kdWxlLmFkZEV2ZW50TGlzdGVuZXIoJ2NsaWNrJywgdGhpcy5kZWJvdW5jZSk7XG59O1xuXG4vKipcbiAqIEphdmFTY3JpcHQgJ3BvbHlmaWxsJyBmb3IgSFRNTDUncyA8ZGV0YWlscz4gYW5kIDxzdW1tYXJ5PiBlbGVtZW50c1xuICogYW5kICdzaGltJyB0byBhZGQgYWNjZXNzaWJsaXR5IGVuaGFuY2VtZW50cyBmb3IgYWxsIGJyb3dzZXJzXG4gKlxuICogaHR0cDovL2Nhbml1c2UuY29tLyNmZWF0PWRldGFpbHNcbiAqL1xuXG52YXIgS0VZX0VOVEVSID0gMTM7XG52YXIgS0VZX1NQQUNFJDEgPSAzMjtcblxuZnVuY3Rpb24gRGV0YWlscyAoJG1vZHVsZSkge1xuICB0aGlzLiRtb2R1bGUgPSAkbW9kdWxlO1xufVxuXG5EZXRhaWxzLnByb3RvdHlwZS5pbml0ID0gZnVuY3Rpb24gKCkge1xuICBpZiAoIXRoaXMuJG1vZHVsZSkge1xuICAgIHJldHVyblxuICB9XG5cbiAgLy8gSWYgdGhlcmUgaXMgbmF0aXZlIGRldGFpbHMgc3VwcG9ydCwgd2Ugd2FudCB0byBhdm9pZCBydW5uaW5nIGNvZGUgdG8gcG9seWZpbGwgbmF0aXZlIGJlaGF2aW91ci5cbiAgdmFyIGhhc05hdGl2ZURldGFpbHMgPSB0eXBlb2YgdGhpcy4kbW9kdWxlLm9wZW4gPT09ICdib29sZWFuJztcblxuICBpZiAoaGFzTmF0aXZlRGV0YWlscykge1xuICAgIHJldHVyblxuICB9XG5cbiAgdGhpcy5wb2x5ZmlsbERldGFpbHMoKTtcbn07XG5cbkRldGFpbHMucHJvdG90eXBlLnBvbHlmaWxsRGV0YWlscyA9IGZ1bmN0aW9uICgpIHtcbiAgdmFyICRtb2R1bGUgPSB0aGlzLiRtb2R1bGU7XG5cbiAgLy8gU2F2ZSBzaG9ydGN1dHMgdG8gdGhlIGlubmVyIHN1bW1hcnkgYW5kIGNvbnRlbnQgZWxlbWVudHNcbiAgdmFyICRzdW1tYXJ5ID0gdGhpcy4kc3VtbWFyeSA9ICRtb2R1bGUuZ2V0RWxlbWVudHNCeVRhZ05hbWUoJ3N1bW1hcnknKS5pdGVtKDApO1xuICB2YXIgJGNvbnRlbnQgPSB0aGlzLiRjb250ZW50ID0gJG1vZHVsZS5nZXRFbGVtZW50c0J5VGFnTmFtZSgnZGl2JykuaXRlbSgwKTtcblxuICAvLyBJZiA8ZGV0YWlscz4gZG9lc24ndCBoYXZlIGEgPHN1bW1hcnk+IGFuZCBhIDxkaXY+IHJlcHJlc2VudGluZyB0aGUgY29udGVudFxuICAvLyBpdCBtZWFucyB0aGUgcmVxdWlyZWQgSFRNTCBzdHJ1Y3R1cmUgaXMgbm90IG1ldCBzbyB0aGUgc2NyaXB0IHdpbGwgc3RvcFxuICBpZiAoISRzdW1tYXJ5IHx8ICEkY29udGVudCkge1xuICAgIHJldHVyblxuICB9XG5cbiAgLy8gSWYgdGhlIGNvbnRlbnQgZG9lc24ndCBoYXZlIGFuIElELCBhc3NpZ24gaXQgb25lIG5vd1xuICAvLyB3aGljaCB3ZSdsbCBuZWVkIGZvciB0aGUgc3VtbWFyeSdzIGFyaWEtY29udHJvbHMgYXNzaWdubWVudFxuICBpZiAoISRjb250ZW50LmlkKSB7XG4gICAgJGNvbnRlbnQuaWQgPSAnZGV0YWlscy1jb250ZW50LScgKyBnZW5lcmF0ZVVuaXF1ZUlEKCk7XG4gIH1cblxuICAvLyBBZGQgQVJJQSByb2xlPVwiZ3JvdXBcIiB0byBkZXRhaWxzXG4gICRtb2R1bGUuc2V0QXR0cmlidXRlKCdyb2xlJywgJ2dyb3VwJyk7XG5cbiAgLy8gQWRkIHJvbGU9YnV0dG9uIHRvIHN1bW1hcnlcbiAgJHN1bW1hcnkuc2V0QXR0cmlidXRlKCdyb2xlJywgJ2J1dHRvbicpO1xuXG4gIC8vIEFkZCBhcmlhLWNvbnRyb2xzXG4gICRzdW1tYXJ5LnNldEF0dHJpYnV0ZSgnYXJpYS1jb250cm9scycsICRjb250ZW50LmlkKTtcblxuICAvLyBTZXQgdGFiSW5kZXggc28gdGhlIHN1bW1hcnkgaXMga2V5Ym9hcmQgYWNjZXNzaWJsZSBmb3Igbm9uLW5hdGl2ZSBlbGVtZW50c1xuICAvL1xuICAvLyBXZSBoYXZlIHRvIHVzZSB0aGUgY2FtZWxjYXNlIGB0YWJJbmRleGAgcHJvcGVydHkgYXMgdGhlcmUgaXMgYSBidWcgaW4gSUU2L0lFNyB3aGVuIHdlIHNldCB0aGUgY29ycmVjdCBhdHRyaWJ1dGUgbG93ZXJjYXNlOlxuICAvLyBTZWUgaHR0cDovL3dlYi5hcmNoaXZlLm9yZy93ZWIvMjAxNzAxMjAxOTQwMzYvaHR0cDovL3d3dy5zYWxpZW5jZXMuY29tL2Jyb3dzZXJCdWdzL3RhYkluZGV4Lmh0bWwgZm9yIG1vcmUgaW5mb3JtYXRpb24uXG4gICRzdW1tYXJ5LnRhYkluZGV4ID0gMDtcblxuICAvLyBEZXRlY3QgaW5pdGlhbCBvcGVuIHN0YXRlXG4gIHZhciBvcGVuQXR0ciA9ICRtb2R1bGUuZ2V0QXR0cmlidXRlKCdvcGVuJykgIT09IG51bGw7XG4gIGlmIChvcGVuQXR0ciA9PT0gdHJ1ZSkge1xuICAgICRzdW1tYXJ5LnNldEF0dHJpYnV0ZSgnYXJpYS1leHBhbmRlZCcsICd0cnVlJyk7XG4gICAgJGNvbnRlbnQuc2V0QXR0cmlidXRlKCdhcmlhLWhpZGRlbicsICdmYWxzZScpO1xuICB9IGVsc2Uge1xuICAgICRzdW1tYXJ5LnNldEF0dHJpYnV0ZSgnYXJpYS1leHBhbmRlZCcsICdmYWxzZScpO1xuICAgICRjb250ZW50LnNldEF0dHJpYnV0ZSgnYXJpYS1oaWRkZW4nLCAndHJ1ZScpO1xuICAgICRjb250ZW50LnN0eWxlLmRpc3BsYXkgPSAnbm9uZSc7XG4gIH1cblxuICAvLyBCaW5kIGFuIGV2ZW50IHRvIGhhbmRsZSBzdW1tYXJ5IGVsZW1lbnRzXG4gIHRoaXMucG9seWZpbGxIYW5kbGVJbnB1dHMoJHN1bW1hcnksIHRoaXMucG9seWZpbGxTZXRBdHRyaWJ1dGVzLmJpbmQodGhpcykpO1xufTtcblxuLyoqXG4qIERlZmluZSBhIHN0YXRlY2hhbmdlIGZ1bmN0aW9uIHRoYXQgdXBkYXRlcyBhcmlhLWV4cGFuZGVkIGFuZCBzdHlsZS5kaXNwbGF5XG4qIEBwYXJhbSB7b2JqZWN0fSBzdW1tYXJ5IGVsZW1lbnRcbiovXG5EZXRhaWxzLnByb3RvdHlwZS5wb2x5ZmlsbFNldEF0dHJpYnV0ZXMgPSBmdW5jdGlvbiAoKSB7XG4gIHZhciAkbW9kdWxlID0gdGhpcy4kbW9kdWxlO1xuICB2YXIgJHN1bW1hcnkgPSB0aGlzLiRzdW1tYXJ5O1xuICB2YXIgJGNvbnRlbnQgPSB0aGlzLiRjb250ZW50O1xuXG4gIHZhciBleHBhbmRlZCA9ICRzdW1tYXJ5LmdldEF0dHJpYnV0ZSgnYXJpYS1leHBhbmRlZCcpID09PSAndHJ1ZSc7XG4gIHZhciBoaWRkZW4gPSAkY29udGVudC5nZXRBdHRyaWJ1dGUoJ2FyaWEtaGlkZGVuJykgPT09ICd0cnVlJztcblxuICAkc3VtbWFyeS5zZXRBdHRyaWJ1dGUoJ2FyaWEtZXhwYW5kZWQnLCAoZXhwYW5kZWQgPyAnZmFsc2UnIDogJ3RydWUnKSk7XG4gICRjb250ZW50LnNldEF0dHJpYnV0ZSgnYXJpYS1oaWRkZW4nLCAoaGlkZGVuID8gJ2ZhbHNlJyA6ICd0cnVlJykpO1xuXG4gICRjb250ZW50LnN0eWxlLmRpc3BsYXkgPSAoZXhwYW5kZWQgPyAnbm9uZScgOiAnJyk7XG5cbiAgdmFyIGhhc09wZW5BdHRyID0gJG1vZHVsZS5nZXRBdHRyaWJ1dGUoJ29wZW4nKSAhPT0gbnVsbDtcbiAgaWYgKCFoYXNPcGVuQXR0cikge1xuICAgICRtb2R1bGUuc2V0QXR0cmlidXRlKCdvcGVuJywgJ29wZW4nKTtcbiAgfSBlbHNlIHtcbiAgICAkbW9kdWxlLnJlbW92ZUF0dHJpYnV0ZSgnb3BlbicpO1xuICB9XG5cbiAgcmV0dXJuIHRydWVcbn07XG5cbi8qKlxuKiBIYW5kbGUgY3Jvc3MtbW9kYWwgY2xpY2sgZXZlbnRzXG4qIEBwYXJhbSB7b2JqZWN0fSBub2RlIGVsZW1lbnRcbiogQHBhcmFtIHtmdW5jdGlvbn0gY2FsbGJhY2sgZnVuY3Rpb25cbiovXG5EZXRhaWxzLnByb3RvdHlwZS5wb2x5ZmlsbEhhbmRsZUlucHV0cyA9IGZ1bmN0aW9uIChub2RlLCBjYWxsYmFjaykge1xuICBub2RlLmFkZEV2ZW50TGlzdGVuZXIoJ2tleXByZXNzJywgZnVuY3Rpb24gKGV2ZW50KSB7XG4gICAgdmFyIHRhcmdldCA9IGV2ZW50LnRhcmdldDtcbiAgICAvLyBXaGVuIHRoZSBrZXkgZ2V0cyBwcmVzc2VkIC0gY2hlY2sgaWYgaXQgaXMgZW50ZXIgb3Igc3BhY2VcbiAgICBpZiAoZXZlbnQua2V5Q29kZSA9PT0gS0VZX0VOVEVSIHx8IGV2ZW50LmtleUNvZGUgPT09IEtFWV9TUEFDRSQxKSB7XG4gICAgICBpZiAodGFyZ2V0Lm5vZGVOYW1lLnRvTG93ZXJDYXNlKCkgPT09ICdzdW1tYXJ5Jykge1xuICAgICAgICAvLyBQcmV2ZW50IHNwYWNlIGZyb20gc2Nyb2xsaW5nIHRoZSBwYWdlXG4gICAgICAgIC8vIGFuZCBlbnRlciBmcm9tIHN1Ym1pdHRpbmcgYSBmb3JtXG4gICAgICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XG4gICAgICAgIC8vIENsaWNrIHRvIGxldCB0aGUgY2xpY2sgZXZlbnQgZG8gYWxsIHRoZSBuZWNlc3NhcnkgYWN0aW9uXG4gICAgICAgIGlmICh0YXJnZXQuY2xpY2spIHtcbiAgICAgICAgICB0YXJnZXQuY2xpY2soKTtcbiAgICAgICAgfSBlbHNlIHtcbiAgICAgICAgICAvLyBleGNlcHQgU2FmYXJpIDUuMSBhbmQgdW5kZXIgZG9uJ3Qgc3VwcG9ydCAuY2xpY2soKSBoZXJlXG4gICAgICAgICAgY2FsbGJhY2soZXZlbnQpO1xuICAgICAgICB9XG4gICAgICB9XG4gICAgfVxuICB9KTtcblxuICAvLyBQcmV2ZW50IGtleXVwIHRvIHByZXZlbnQgY2xpY2tpbmcgdHdpY2UgaW4gRmlyZWZveCB3aGVuIHVzaW5nIHNwYWNlIGtleVxuICBub2RlLmFkZEV2ZW50TGlzdGVuZXIoJ2tleXVwJywgZnVuY3Rpb24gKGV2ZW50KSB7XG4gICAgdmFyIHRhcmdldCA9IGV2ZW50LnRhcmdldDtcbiAgICBpZiAoZXZlbnQua2V5Q29kZSA9PT0gS0VZX1NQQUNFJDEpIHtcbiAgICAgIGlmICh0YXJnZXQubm9kZU5hbWUudG9Mb3dlckNhc2UoKSA9PT0gJ3N1bW1hcnknKSB7XG4gICAgICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XG4gICAgICB9XG4gICAgfVxuICB9KTtcblxuICBub2RlLmFkZEV2ZW50TGlzdGVuZXIoJ2NsaWNrJywgY2FsbGJhY2spO1xufTtcblxuZnVuY3Rpb24gQ2hhcmFjdGVyQ291bnQgKCRtb2R1bGUpIHtcbiAgdGhpcy4kbW9kdWxlID0gJG1vZHVsZTtcbiAgdGhpcy4kdGV4dGFyZWEgPSAkbW9kdWxlLnF1ZXJ5U2VsZWN0b3IoJy5nb3Z1ay1qcy1jaGFyYWN0ZXItY291bnQnKTtcbiAgaWYgKHRoaXMuJHRleHRhcmVhKSB7XG4gICAgdGhpcy4kY291bnRNZXNzYWdlID0gJG1vZHVsZS5xdWVyeVNlbGVjdG9yKCdbaWQ9JyArIHRoaXMuJHRleHRhcmVhLmlkICsgJy1pbmZvXScpO1xuICB9XG59XG5cbkNoYXJhY3RlckNvdW50LnByb3RvdHlwZS5kZWZhdWx0cyA9IHtcbiAgY2hhcmFjdGVyQ291bnRBdHRyaWJ1dGU6ICdkYXRhLW1heGxlbmd0aCcsXG4gIHdvcmRDb3VudEF0dHJpYnV0ZTogJ2RhdGEtbWF4d29yZHMnXG59O1xuXG4vLyBJbml0aWFsaXplIGNvbXBvbmVudFxuQ2hhcmFjdGVyQ291bnQucHJvdG90eXBlLmluaXQgPSBmdW5jdGlvbiAoKSB7XG4gIC8vIENoZWNrIGZvciBtb2R1bGVcbiAgdmFyICRtb2R1bGUgPSB0aGlzLiRtb2R1bGU7XG4gIHZhciAkdGV4dGFyZWEgPSB0aGlzLiR0ZXh0YXJlYTtcbiAgdmFyICRjb3VudE1lc3NhZ2UgPSB0aGlzLiRjb3VudE1lc3NhZ2U7XG5cbiAgaWYgKCEkdGV4dGFyZWEgfHwgISRjb3VudE1lc3NhZ2UpIHtcbiAgICByZXR1cm5cbiAgfVxuXG4gIC8vIFdlIG1vdmUgY291bnQgbWVzc2FnZSByaWdodCBhZnRlciB0aGUgZmllbGRcbiAgLy8gS2VwdCBmb3IgYmFja3dhcmRzIGNvbXBhdGliaWxpdHlcbiAgJHRleHRhcmVhLmluc2VydEFkamFjZW50RWxlbWVudCgnYWZ0ZXJlbmQnLCAkY291bnRNZXNzYWdlKTtcblxuICAvLyBSZWFkIG9wdGlvbnMgc2V0IHVzaW5nIGRhdGFzZXQgKCdkYXRhLScgdmFsdWVzKVxuICB0aGlzLm9wdGlvbnMgPSB0aGlzLmdldERhdGFzZXQoJG1vZHVsZSk7XG5cbiAgLy8gRGV0ZXJtaW5lIHRoZSBsaW1pdCBhdHRyaWJ1dGUgKGNoYXJhY3RlcnMgb3Igd29yZHMpXG4gIHZhciBjb3VudEF0dHJpYnV0ZSA9IHRoaXMuZGVmYXVsdHMuY2hhcmFjdGVyQ291bnRBdHRyaWJ1dGU7XG4gIGlmICh0aGlzLm9wdGlvbnMubWF4d29yZHMpIHtcbiAgICBjb3VudEF0dHJpYnV0ZSA9IHRoaXMuZGVmYXVsdHMud29yZENvdW50QXR0cmlidXRlO1xuICB9XG5cbiAgLy8gU2F2ZSB0aGUgZWxlbWVudCBsaW1pdFxuICB0aGlzLm1heExlbmd0aCA9ICRtb2R1bGUuZ2V0QXR0cmlidXRlKGNvdW50QXR0cmlidXRlKTtcblxuICAvLyBDaGVjayBmb3IgbGltaXRcbiAgaWYgKCF0aGlzLm1heExlbmd0aCkge1xuICAgIHJldHVyblxuICB9XG5cbiAgLy8gUmVtb3ZlIGhhcmQgbGltaXQgaWYgc2V0XG4gICRtb2R1bGUucmVtb3ZlQXR0cmlidXRlKCdtYXhsZW5ndGgnKTtcblxuICAvLyBCaW5kIGV2ZW50IGNoYW5nZXMgdG8gdGhlIHRleHRhcmVhXG4gIHZhciBib3VuZENoYW5nZUV2ZW50cyA9IHRoaXMuYmluZENoYW5nZUV2ZW50cy5iaW5kKHRoaXMpO1xuICBib3VuZENoYW5nZUV2ZW50cygpO1xuXG4gIC8vIFVwZGF0ZSBjb3VudCBtZXNzYWdlXG4gIHZhciBib3VuZFVwZGF0ZUNvdW50TWVzc2FnZSA9IHRoaXMudXBkYXRlQ291bnRNZXNzYWdlLmJpbmQodGhpcyk7XG4gIGJvdW5kVXBkYXRlQ291bnRNZXNzYWdlKCk7XG59O1xuXG4vLyBSZWFkIGRhdGEgYXR0cmlidXRlc1xuQ2hhcmFjdGVyQ291bnQucHJvdG90eXBlLmdldERhdGFzZXQgPSBmdW5jdGlvbiAoZWxlbWVudCkge1xuICB2YXIgZGF0YXNldCA9IHt9O1xuICB2YXIgYXR0cmlidXRlcyA9IGVsZW1lbnQuYXR0cmlidXRlcztcbiAgaWYgKGF0dHJpYnV0ZXMpIHtcbiAgICBmb3IgKHZhciBpID0gMDsgaSA8IGF0dHJpYnV0ZXMubGVuZ3RoOyBpKyspIHtcbiAgICAgIHZhciBhdHRyaWJ1dGUgPSBhdHRyaWJ1dGVzW2ldO1xuICAgICAgdmFyIG1hdGNoID0gYXR0cmlidXRlLm5hbWUubWF0Y2goL15kYXRhLSguKykvKTtcbiAgICAgIGlmIChtYXRjaCkge1xuICAgICAgICBkYXRhc2V0W21hdGNoWzFdXSA9IGF0dHJpYnV0ZS52YWx1ZTtcbiAgICAgIH1cbiAgICB9XG4gIH1cbiAgcmV0dXJuIGRhdGFzZXRcbn07XG5cbi8vIENvdW50cyBjaGFyYWN0ZXJzIG9yIHdvcmRzIGluIHRleHRcbkNoYXJhY3RlckNvdW50LnByb3RvdHlwZS5jb3VudCA9IGZ1bmN0aW9uICh0ZXh0KSB7XG4gIHZhciBsZW5ndGg7XG4gIGlmICh0aGlzLm9wdGlvbnMubWF4d29yZHMpIHtcbiAgICB2YXIgdG9rZW5zID0gdGV4dC5tYXRjaCgvXFxTKy9nKSB8fCBbXTsgLy8gTWF0Y2hlcyBjb25zZWN1dGl2ZSBub24td2hpdGVzcGFjZSBjaGFyc1xuICAgIGxlbmd0aCA9IHRva2Vucy5sZW5ndGg7XG4gIH0gZWxzZSB7XG4gICAgbGVuZ3RoID0gdGV4dC5sZW5ndGg7XG4gIH1cbiAgcmV0dXJuIGxlbmd0aFxufTtcblxuLy8gQmluZCBpbnB1dCBwcm9wZXJ0eWNoYW5nZSB0byB0aGUgZWxlbWVudHMgYW5kIHVwZGF0ZSBiYXNlZCBvbiB0aGUgY2hhbmdlXG5DaGFyYWN0ZXJDb3VudC5wcm90b3R5cGUuYmluZENoYW5nZUV2ZW50cyA9IGZ1bmN0aW9uICgpIHtcbiAgdmFyICR0ZXh0YXJlYSA9IHRoaXMuJHRleHRhcmVhO1xuICAkdGV4dGFyZWEuYWRkRXZlbnRMaXN0ZW5lcigna2V5dXAnLCB0aGlzLmNoZWNrSWZWYWx1ZUNoYW5nZWQuYmluZCh0aGlzKSk7XG5cbiAgLy8gQmluZCBmb2N1cy9ibHVyIGV2ZW50cyB0byBzdGFydC9zdG9wIHBvbGxpbmdcbiAgJHRleHRhcmVhLmFkZEV2ZW50TGlzdGVuZXIoJ2ZvY3VzJywgdGhpcy5oYW5kbGVGb2N1cy5iaW5kKHRoaXMpKTtcbiAgJHRleHRhcmVhLmFkZEV2ZW50TGlzdGVuZXIoJ2JsdXInLCB0aGlzLmhhbmRsZUJsdXIuYmluZCh0aGlzKSk7XG59O1xuXG4vLyBTcGVlY2ggcmVjb2duaXRpb24gc29mdHdhcmUgc3VjaCBhcyBEcmFnb24gTmF0dXJhbGx5U3BlYWtpbmcgd2lsbCBtb2RpZnkgdGhlXG4vLyBmaWVsZHMgYnkgZGlyZWN0bHkgY2hhbmdpbmcgaXRzIGB2YWx1ZWAuIFRoZXNlIGNoYW5nZXMgZG9uJ3QgdHJpZ2dlciBldmVudHNcbi8vIGluIEphdmFTY3JpcHQsIHNvIHdlIG5lZWQgdG8gcG9sbCB0byBoYW5kbGUgd2hlbiBhbmQgaWYgdGhleSBvY2N1ci5cbkNoYXJhY3RlckNvdW50LnByb3RvdHlwZS5jaGVja0lmVmFsdWVDaGFuZ2VkID0gZnVuY3Rpb24gKCkge1xuICBpZiAoIXRoaXMuJHRleHRhcmVhLm9sZFZhbHVlKSB0aGlzLiR0ZXh0YXJlYS5vbGRWYWx1ZSA9ICcnO1xuICBpZiAodGhpcy4kdGV4dGFyZWEudmFsdWUgIT09IHRoaXMuJHRleHRhcmVhLm9sZFZhbHVlKSB7XG4gICAgdGhpcy4kdGV4dGFyZWEub2xkVmFsdWUgPSB0aGlzLiR0ZXh0YXJlYS52YWx1ZTtcbiAgICB2YXIgYm91bmRVcGRhdGVDb3VudE1lc3NhZ2UgPSB0aGlzLnVwZGF0ZUNvdW50TWVzc2FnZS5iaW5kKHRoaXMpO1xuICAgIGJvdW5kVXBkYXRlQ291bnRNZXNzYWdlKCk7XG4gIH1cbn07XG5cbi8vIFVwZGF0ZSBtZXNzYWdlIGJveFxuQ2hhcmFjdGVyQ291bnQucHJvdG90eXBlLnVwZGF0ZUNvdW50TWVzc2FnZSA9IGZ1bmN0aW9uICgpIHtcbiAgdmFyIGNvdW50RWxlbWVudCA9IHRoaXMuJHRleHRhcmVhO1xuICB2YXIgb3B0aW9ucyA9IHRoaXMub3B0aW9ucztcbiAgdmFyIGNvdW50TWVzc2FnZSA9IHRoaXMuJGNvdW50TWVzc2FnZTtcblxuICAvLyBEZXRlcm1pbmUgdGhlIHJlbWFpbmluZyBudW1iZXIgb2YgY2hhcmFjdGVycy93b3Jkc1xuICB2YXIgY3VycmVudExlbmd0aCA9IHRoaXMuY291bnQoY291bnRFbGVtZW50LnZhbHVlKTtcbiAgdmFyIG1heExlbmd0aCA9IHRoaXMubWF4TGVuZ3RoO1xuICB2YXIgcmVtYWluaW5nTnVtYmVyID0gbWF4TGVuZ3RoIC0gY3VycmVudExlbmd0aDtcblxuICAvLyBTZXQgdGhyZXNob2xkIGlmIHByZXNlbnRlZCBpbiBvcHRpb25zXG4gIHZhciB0aHJlc2hvbGRQZXJjZW50ID0gb3B0aW9ucy50aHJlc2hvbGQgPyBvcHRpb25zLnRocmVzaG9sZCA6IDA7XG4gIHZhciB0aHJlc2hvbGRWYWx1ZSA9IG1heExlbmd0aCAqIHRocmVzaG9sZFBlcmNlbnQgLyAxMDA7XG4gIGlmICh0aHJlc2hvbGRWYWx1ZSA+IGN1cnJlbnRMZW5ndGgpIHtcbiAgICBjb3VudE1lc3NhZ2UuY2xhc3NMaXN0LmFkZCgnZ292dWstY2hhcmFjdGVyLWNvdW50X19tZXNzYWdlLS1kaXNhYmxlZCcpO1xuICAgIC8vIEVuc3VyZSB0aHJlc2hvbGQgaXMgaGlkZGVuIGZvciB1c2VycyBvZiBhc3Npc3RpdmUgdGVjaG5vbG9naWVzXG4gICAgY291bnRNZXNzYWdlLnNldEF0dHJpYnV0ZSgnYXJpYS1oaWRkZW4nLCB0cnVlKTtcbiAgfSBlbHNlIHtcbiAgICBjb3VudE1lc3NhZ2UuY2xhc3NMaXN0LnJlbW92ZSgnZ292dWstY2hhcmFjdGVyLWNvdW50X19tZXNzYWdlLS1kaXNhYmxlZCcpO1xuICAgIC8vIEVuc3VyZSB0aHJlc2hvbGQgaXMgdmlzaWJsZSBmb3IgdXNlcnMgb2YgYXNzaXN0aXZlIHRlY2hub2xvZ2llc1xuICAgIGNvdW50TWVzc2FnZS5yZW1vdmVBdHRyaWJ1dGUoJ2FyaWEtaGlkZGVuJyk7XG4gIH1cblxuICAvLyBVcGRhdGUgc3R5bGVzXG4gIGlmIChyZW1haW5pbmdOdW1iZXIgPCAwKSB7XG4gICAgY291bnRFbGVtZW50LmNsYXNzTGlzdC5hZGQoJ2dvdnVrLXRleHRhcmVhLS1lcnJvcicpO1xuICAgIGNvdW50TWVzc2FnZS5jbGFzc0xpc3QucmVtb3ZlKCdnb3Z1ay1oaW50Jyk7XG4gICAgY291bnRNZXNzYWdlLmNsYXNzTGlzdC5hZGQoJ2dvdnVrLWVycm9yLW1lc3NhZ2UnKTtcbiAgfSBlbHNlIHtcbiAgICBjb3VudEVsZW1lbnQuY2xhc3NMaXN0LnJlbW92ZSgnZ292dWstdGV4dGFyZWEtLWVycm9yJyk7XG4gICAgY291bnRNZXNzYWdlLmNsYXNzTGlzdC5yZW1vdmUoJ2dvdnVrLWVycm9yLW1lc3NhZ2UnKTtcbiAgICBjb3VudE1lc3NhZ2UuY2xhc3NMaXN0LmFkZCgnZ292dWstaGludCcpO1xuICB9XG5cbiAgLy8gVXBkYXRlIG1lc3NhZ2VcbiAgdmFyIGNoYXJWZXJiID0gJ3JlbWFpbmluZyc7XG4gIHZhciBjaGFyTm91biA9ICdjaGFyYWN0ZXInO1xuICB2YXIgZGlzcGxheU51bWJlciA9IHJlbWFpbmluZ051bWJlcjtcbiAgaWYgKG9wdGlvbnMubWF4d29yZHMpIHtcbiAgICBjaGFyTm91biA9ICd3b3JkJztcbiAgfVxuICBjaGFyTm91biA9IGNoYXJOb3VuICsgKChyZW1haW5pbmdOdW1iZXIgPT09IC0xIHx8IHJlbWFpbmluZ051bWJlciA9PT0gMSkgPyAnJyA6ICdzJyk7XG5cbiAgY2hhclZlcmIgPSAocmVtYWluaW5nTnVtYmVyIDwgMCkgPyAndG9vIG1hbnknIDogJ3JlbWFpbmluZyc7XG4gIGRpc3BsYXlOdW1iZXIgPSBNYXRoLmFicyhyZW1haW5pbmdOdW1iZXIpO1xuXG4gIGNvdW50TWVzc2FnZS5pbm5lckhUTUwgPSAnWW91IGhhdmUgJyArIGRpc3BsYXlOdW1iZXIgKyAnICcgKyBjaGFyTm91biArICcgJyArIGNoYXJWZXJiO1xufTtcblxuQ2hhcmFjdGVyQ291bnQucHJvdG90eXBlLmhhbmRsZUZvY3VzID0gZnVuY3Rpb24gKCkge1xuICAvLyBDaGVjayBpZiB2YWx1ZSBjaGFuZ2VkIG9uIGZvY3VzXG4gIHRoaXMudmFsdWVDaGVja2VyID0gc2V0SW50ZXJ2YWwodGhpcy5jaGVja0lmVmFsdWVDaGFuZ2VkLmJpbmQodGhpcyksIDEwMDApO1xufTtcblxuQ2hhcmFjdGVyQ291bnQucHJvdG90eXBlLmhhbmRsZUJsdXIgPSBmdW5jdGlvbiAoKSB7XG4gIC8vIENhbmNlbCB2YWx1ZSBjaGVja2luZyBvbiBibHVyXG4gIGNsZWFySW50ZXJ2YWwodGhpcy52YWx1ZUNoZWNrZXIpO1xufTtcblxuZnVuY3Rpb24gQ2hlY2tib3hlcyAoJG1vZHVsZSkge1xuICB0aGlzLiRtb2R1bGUgPSAkbW9kdWxlO1xuICB0aGlzLiRpbnB1dHMgPSAkbW9kdWxlLnF1ZXJ5U2VsZWN0b3JBbGwoJ2lucHV0W3R5cGU9XCJjaGVja2JveFwiXScpO1xufVxuXG5DaGVja2JveGVzLnByb3RvdHlwZS5pbml0ID0gZnVuY3Rpb24gKCkge1xuICB2YXIgJG1vZHVsZSA9IHRoaXMuJG1vZHVsZTtcbiAgdmFyICRpbnB1dHMgPSB0aGlzLiRpbnB1dHM7XG5cbiAgLyoqXG4gICogTG9vcCBvdmVyIGFsbCBpdGVtcyB3aXRoIFtkYXRhLWNvbnRyb2xzXVxuICAqIENoZWNrIGlmIHRoZXkgaGF2ZSBhIG1hdGNoaW5nIGNvbmRpdGlvbmFsIHJldmVhbFxuICAqIElmIHRoZXkgZG8sIGFzc2lnbiBhdHRyaWJ1dGVzLlxuICAqKi9cbiAgbm9kZUxpc3RGb3JFYWNoKCRpbnB1dHMsIGZ1bmN0aW9uICgkaW5wdXQpIHtcbiAgICB2YXIgY29udHJvbHMgPSAkaW5wdXQuZ2V0QXR0cmlidXRlKCdkYXRhLWFyaWEtY29udHJvbHMnKTtcblxuICAgIC8vIENoZWNrIGlmIGlucHV0IGNvbnRyb2xzIGFueXRoaW5nXG4gICAgLy8gQ2hlY2sgaWYgY29udGVudCBleGlzdHMsIGJlZm9yZSBzZXR0aW5nIGF0dHJpYnV0ZXMuXG4gICAgaWYgKCFjb250cm9scyB8fCAhJG1vZHVsZS5xdWVyeVNlbGVjdG9yKCcjJyArIGNvbnRyb2xzKSkge1xuICAgICAgcmV0dXJuXG4gICAgfVxuXG4gICAgLy8gSWYgd2UgaGF2ZSBjb250ZW50IHRoYXQgaXMgY29udHJvbGxlZCwgc2V0IGF0dHJpYnV0ZXMuXG4gICAgJGlucHV0LnNldEF0dHJpYnV0ZSgnYXJpYS1jb250cm9scycsIGNvbnRyb2xzKTtcbiAgICAkaW5wdXQucmVtb3ZlQXR0cmlidXRlKCdkYXRhLWFyaWEtY29udHJvbHMnKTtcbiAgICB0aGlzLnNldEF0dHJpYnV0ZXMoJGlucHV0KTtcbiAgfS5iaW5kKHRoaXMpKTtcblxuICAvLyBIYW5kbGUgZXZlbnRzXG4gICRtb2R1bGUuYWRkRXZlbnRMaXN0ZW5lcignY2xpY2snLCB0aGlzLmhhbmRsZUNsaWNrLmJpbmQodGhpcykpO1xufTtcblxuQ2hlY2tib3hlcy5wcm90b3R5cGUuc2V0QXR0cmlidXRlcyA9IGZ1bmN0aW9uICgkaW5wdXQpIHtcbiAgdmFyIGlucHV0SXNDaGVja2VkID0gJGlucHV0LmNoZWNrZWQ7XG4gICRpbnB1dC5zZXRBdHRyaWJ1dGUoJ2FyaWEtZXhwYW5kZWQnLCBpbnB1dElzQ2hlY2tlZCk7XG5cbiAgdmFyICRjb250ZW50ID0gdGhpcy4kbW9kdWxlLnF1ZXJ5U2VsZWN0b3IoJyMnICsgJGlucHV0LmdldEF0dHJpYnV0ZSgnYXJpYS1jb250cm9scycpKTtcbiAgaWYgKCRjb250ZW50KSB7XG4gICAgJGNvbnRlbnQuY2xhc3NMaXN0LnRvZ2dsZSgnZ292dWstY2hlY2tib3hlc19fY29uZGl0aW9uYWwtLWhpZGRlbicsICFpbnB1dElzQ2hlY2tlZCk7XG4gIH1cbn07XG5cbkNoZWNrYm94ZXMucHJvdG90eXBlLmhhbmRsZUNsaWNrID0gZnVuY3Rpb24gKGV2ZW50KSB7XG4gIHZhciAkdGFyZ2V0ID0gZXZlbnQudGFyZ2V0O1xuXG4gIC8vIElmIGEgY2hlY2tib3ggd2l0aCBhcmlhLWNvbnRyb2xzLCBoYW5kbGUgY2xpY2tcbiAgdmFyIGlzQ2hlY2tib3ggPSAkdGFyZ2V0LmdldEF0dHJpYnV0ZSgndHlwZScpID09PSAnY2hlY2tib3gnO1xuICB2YXIgaGFzQXJpYUNvbnRyb2xzID0gJHRhcmdldC5nZXRBdHRyaWJ1dGUoJ2FyaWEtY29udHJvbHMnKTtcbiAgaWYgKGlzQ2hlY2tib3ggJiYgaGFzQXJpYUNvbnRyb2xzKSB7XG4gICAgdGhpcy5zZXRBdHRyaWJ1dGVzKCR0YXJnZXQpO1xuICB9XG59O1xuXG4oZnVuY3Rpb24odW5kZWZpbmVkKSB7XG5cbiAgLy8gRGV0ZWN0aW9uIGZyb20gaHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlLzFmM2MwOWI0MDJmNjViZjZlMzkzZjkzM2ExNWJhNjNmMWI4NmVmMWYvcGFja2FnZXMvcG9seWZpbGwtbGlicmFyeS9wb2x5ZmlsbHMvRWxlbWVudC9wcm90b3R5cGUvbWF0Y2hlcy9kZXRlY3QuanNcbiAgdmFyIGRldGVjdCA9IChcbiAgICAnZG9jdW1lbnQnIGluIHRoaXMgJiYgXCJtYXRjaGVzXCIgaW4gZG9jdW1lbnQuZG9jdW1lbnRFbGVtZW50XG4gICk7XG5cbiAgaWYgKGRldGVjdCkgcmV0dXJuXG5cbiAgLy8gUG9seWZpbGwgZnJvbSBodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vRmluYW5jaWFsLVRpbWVzL3BvbHlmaWxsLXNlcnZpY2UvMWYzYzA5YjQwMmY2NWJmNmUzOTNmOTMzYTE1YmE2M2YxYjg2ZWYxZi9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9FbGVtZW50L3Byb3RvdHlwZS9tYXRjaGVzL3BvbHlmaWxsLmpzXG4gIEVsZW1lbnQucHJvdG90eXBlLm1hdGNoZXMgPSBFbGVtZW50LnByb3RvdHlwZS53ZWJraXRNYXRjaGVzU2VsZWN0b3IgfHwgRWxlbWVudC5wcm90b3R5cGUub01hdGNoZXNTZWxlY3RvciB8fCBFbGVtZW50LnByb3RvdHlwZS5tc01hdGNoZXNTZWxlY3RvciB8fCBFbGVtZW50LnByb3RvdHlwZS5tb3pNYXRjaGVzU2VsZWN0b3IgfHwgZnVuY3Rpb24gbWF0Y2hlcyhzZWxlY3Rvcikge1xuICAgIHZhciBlbGVtZW50ID0gdGhpcztcbiAgICB2YXIgZWxlbWVudHMgPSAoZWxlbWVudC5kb2N1bWVudCB8fCBlbGVtZW50Lm93bmVyRG9jdW1lbnQpLnF1ZXJ5U2VsZWN0b3JBbGwoc2VsZWN0b3IpO1xuICAgIHZhciBpbmRleCA9IDA7XG5cbiAgICB3aGlsZSAoZWxlbWVudHNbaW5kZXhdICYmIGVsZW1lbnRzW2luZGV4XSAhPT0gZWxlbWVudCkge1xuICAgICAgKytpbmRleDtcbiAgICB9XG5cbiAgICByZXR1cm4gISFlbGVtZW50c1tpbmRleF07XG4gIH07XG5cbn0pLmNhbGwoJ29iamVjdCcgPT09IHR5cGVvZiB3aW5kb3cgJiYgd2luZG93IHx8ICdvYmplY3QnID09PSB0eXBlb2Ygc2VsZiAmJiBzZWxmIHx8ICdvYmplY3QnID09PSB0eXBlb2YgZ2xvYmFsICYmIGdsb2JhbCB8fCB7fSk7XG5cbihmdW5jdGlvbih1bmRlZmluZWQpIHtcblxuICAvLyBEZXRlY3Rpb24gZnJvbSBodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vRmluYW5jaWFsLVRpbWVzL3BvbHlmaWxsLXNlcnZpY2UvMWYzYzA5YjQwMmY2NWJmNmUzOTNmOTMzYTE1YmE2M2YxYjg2ZWYxZi9wYWNrYWdlcy9wb2x5ZmlsbC1saWJyYXJ5L3BvbHlmaWxscy9FbGVtZW50L3Byb3RvdHlwZS9jbG9zZXN0L2RldGVjdC5qc1xuICB2YXIgZGV0ZWN0ID0gKFxuICAgICdkb2N1bWVudCcgaW4gdGhpcyAmJiBcImNsb3Nlc3RcIiBpbiBkb2N1bWVudC5kb2N1bWVudEVsZW1lbnRcbiAgKTtcblxuICBpZiAoZGV0ZWN0KSByZXR1cm5cblxuICAgIC8vIFBvbHlmaWxsIGZyb20gaHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlLzFmM2MwOWI0MDJmNjViZjZlMzkzZjkzM2ExNWJhNjNmMWI4NmVmMWYvcGFja2FnZXMvcG9seWZpbGwtbGlicmFyeS9wb2x5ZmlsbHMvRWxlbWVudC9wcm90b3R5cGUvY2xvc2VzdC9wb2x5ZmlsbC5qc1xuICBFbGVtZW50LnByb3RvdHlwZS5jbG9zZXN0ID0gZnVuY3Rpb24gY2xvc2VzdChzZWxlY3Rvcikge1xuICAgIHZhciBub2RlID0gdGhpcztcblxuICAgIHdoaWxlIChub2RlKSB7XG4gICAgICBpZiAobm9kZS5tYXRjaGVzKHNlbGVjdG9yKSkgcmV0dXJuIG5vZGU7XG4gICAgICBlbHNlIG5vZGUgPSAnU1ZHRWxlbWVudCcgaW4gd2luZG93ICYmIG5vZGUgaW5zdGFuY2VvZiBTVkdFbGVtZW50ID8gbm9kZS5wYXJlbnROb2RlIDogbm9kZS5wYXJlbnRFbGVtZW50O1xuICAgIH1cblxuICAgIHJldHVybiBudWxsO1xuICB9O1xuXG59KS5jYWxsKCdvYmplY3QnID09PSB0eXBlb2Ygd2luZG93ICYmIHdpbmRvdyB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIHNlbGYgJiYgc2VsZiB8fCAnb2JqZWN0JyA9PT0gdHlwZW9mIGdsb2JhbCAmJiBnbG9iYWwgfHwge30pO1xuXG5mdW5jdGlvbiBFcnJvclN1bW1hcnkgKCRtb2R1bGUpIHtcbiAgdGhpcy4kbW9kdWxlID0gJG1vZHVsZTtcbn1cblxuRXJyb3JTdW1tYXJ5LnByb3RvdHlwZS5pbml0ID0gZnVuY3Rpb24gKCkge1xuICB2YXIgJG1vZHVsZSA9IHRoaXMuJG1vZHVsZTtcbiAgaWYgKCEkbW9kdWxlKSB7XG4gICAgcmV0dXJuXG4gIH1cbiAgJG1vZHVsZS5mb2N1cygpO1xuXG4gICRtb2R1bGUuYWRkRXZlbnRMaXN0ZW5lcignY2xpY2snLCB0aGlzLmhhbmRsZUNsaWNrLmJpbmQodGhpcykpO1xufTtcblxuLyoqXG4qIENsaWNrIGV2ZW50IGhhbmRsZXJcbipcbiogQHBhcmFtIHtNb3VzZUV2ZW50fSBldmVudCAtIENsaWNrIGV2ZW50XG4qL1xuRXJyb3JTdW1tYXJ5LnByb3RvdHlwZS5oYW5kbGVDbGljayA9IGZ1bmN0aW9uIChldmVudCkge1xuICB2YXIgdGFyZ2V0ID0gZXZlbnQudGFyZ2V0O1xuICBpZiAodGhpcy5mb2N1c1RhcmdldCh0YXJnZXQpKSB7XG4gICAgZXZlbnQucHJldmVudERlZmF1bHQoKTtcbiAgfVxufTtcblxuLyoqXG4gKiBGb2N1cyB0aGUgdGFyZ2V0IGVsZW1lbnRcbiAqXG4gKiBCeSBkZWZhdWx0LCB0aGUgYnJvd3NlciB3aWxsIHNjcm9sbCB0aGUgdGFyZ2V0IGludG8gdmlldy4gQmVjYXVzZSBvdXIgbGFiZWxzXG4gKiBvciBsZWdlbmRzIGFwcGVhciBhYm92ZSB0aGUgaW5wdXQsIHRoaXMgbWVhbnMgdGhlIHVzZXIgd2lsbCBiZSBwcmVzZW50ZWQgd2l0aFxuICogYW4gaW5wdXQgd2l0aG91dCBhbnkgY29udGV4dCwgYXMgdGhlIGxhYmVsIG9yIGxlZ2VuZCB3aWxsIGJlIG9mZiB0aGUgdG9wIG9mXG4gKiB0aGUgc2NyZWVuLlxuICpcbiAqIE1hbnVhbGx5IGhhbmRsaW5nIHRoZSBjbGljayBldmVudCwgc2Nyb2xsaW5nIHRoZSBxdWVzdGlvbiBpbnRvIHZpZXcgYW5kIHRoZW5cbiAqIGZvY3Vzc2luZyB0aGUgZWxlbWVudCBzb2x2ZXMgdGhpcy5cbiAqXG4gKiBUaGlzIGFsc28gcmVzdWx0cyBpbiB0aGUgbGFiZWwgYW5kL29yIGxlZ2VuZCBiZWluZyBhbm5vdW5jZWQgY29ycmVjdGx5IGluXG4gKiBOVkRBIChhcyB0ZXN0ZWQgaW4gMjAxOC4zLjIpIC0gd2l0aG91dCB0aGlzIG9ubHkgdGhlIGZpZWxkIHR5cGUgaXMgYW5ub3VuY2VkXG4gKiAoZS5nLiBcIkVkaXQsIGhhcyBhdXRvY29tcGxldGVcIikuXG4gKlxuICogQHBhcmFtIHtIVE1MRWxlbWVudH0gJHRhcmdldCAtIEV2ZW50IHRhcmdldFxuICogQHJldHVybnMge2Jvb2xlYW59IFRydWUgaWYgdGhlIHRhcmdldCB3YXMgYWJsZSB0byBiZSBmb2N1c3NlZFxuICovXG5FcnJvclN1bW1hcnkucHJvdG90eXBlLmZvY3VzVGFyZ2V0ID0gZnVuY3Rpb24gKCR0YXJnZXQpIHtcbiAgLy8gSWYgdGhlIGVsZW1lbnQgdGhhdCB3YXMgY2xpY2tlZCB3YXMgbm90IGEgbGluaywgcmV0dXJuIGVhcmx5XG4gIGlmICgkdGFyZ2V0LnRhZ05hbWUgIT09ICdBJyB8fCAkdGFyZ2V0LmhyZWYgPT09IGZhbHNlKSB7XG4gICAgcmV0dXJuIGZhbHNlXG4gIH1cblxuICB2YXIgaW5wdXRJZCA9IHRoaXMuZ2V0RnJhZ21lbnRGcm9tVXJsKCR0YXJnZXQuaHJlZik7XG4gIHZhciAkaW5wdXQgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZChpbnB1dElkKTtcbiAgaWYgKCEkaW5wdXQpIHtcbiAgICByZXR1cm4gZmFsc2VcbiAgfVxuXG4gIHZhciAkbGVnZW5kT3JMYWJlbCA9IHRoaXMuZ2V0QXNzb2NpYXRlZExlZ2VuZE9yTGFiZWwoJGlucHV0KTtcbiAgaWYgKCEkbGVnZW5kT3JMYWJlbCkge1xuICAgIHJldHVybiBmYWxzZVxuICB9XG5cbiAgLy8gU2Nyb2xsIHRoZSBsZWdlbmQgb3IgbGFiZWwgaW50byB2aWV3ICpiZWZvcmUqIGNhbGxpbmcgZm9jdXMgb24gdGhlIGlucHV0IHRvXG4gIC8vIGF2b2lkIGV4dHJhIHNjcm9sbGluZyBpbiBicm93c2VycyB0aGF0IGRvbid0IHN1cHBvcnQgYHByZXZlbnRTY3JvbGxgICh3aGljaFxuICAvLyBhdCB0aW1lIG9mIHdyaXRpbmcgaXMgbW9zdCBvZiB0aGVtLi4uKVxuICAkbGVnZW5kT3JMYWJlbC5zY3JvbGxJbnRvVmlldygpO1xuICAkaW5wdXQuZm9jdXMoeyBwcmV2ZW50U2Nyb2xsOiB0cnVlIH0pO1xuXG4gIHJldHVybiB0cnVlXG59O1xuXG4vKipcbiAqIEdldCBmcmFnbWVudCBmcm9tIFVSTFxuICpcbiAqIEV4dHJhY3QgdGhlIGZyYWdtZW50IChldmVyeXRoaW5nIGFmdGVyIHRoZSBoYXNoKSBmcm9tIGEgVVJMLCBidXQgbm90IGluY2x1ZGluZ1xuICogdGhlIGhhc2guXG4gKlxuICogQHBhcmFtIHtzdHJpbmd9IHVybCAtIFVSTFxuICogQHJldHVybnMge3N0cmluZ30gRnJhZ21lbnQgZnJvbSBVUkwsIHdpdGhvdXQgdGhlIGhhc2hcbiAqL1xuRXJyb3JTdW1tYXJ5LnByb3RvdHlwZS5nZXRGcmFnbWVudEZyb21VcmwgPSBmdW5jdGlvbiAodXJsKSB7XG4gIGlmICh1cmwuaW5kZXhPZignIycpID09PSAtMSkge1xuICAgIHJldHVybiBmYWxzZVxuICB9XG5cbiAgcmV0dXJuIHVybC5zcGxpdCgnIycpLnBvcCgpXG59O1xuXG4vKipcbiAqIEdldCBhc3NvY2lhdGVkIGxlZ2VuZCBvciBsYWJlbFxuICpcbiAqIFJldHVybnMgdGhlIGZpcnN0IGVsZW1lbnQgdGhhdCBleGlzdHMgZnJvbSB0aGlzIGxpc3Q6XG4gKlxuICogLSBUaGUgYDxsZWdlbmQ+YCBhc3NvY2lhdGVkIHdpdGggdGhlIGNsb3Nlc3QgYDxmaWVsZHNldD5gIGFuY2VzdG9yLCBhcyBsb25nXG4gKiAgIGFzIHRoZSB0b3Agb2YgaXQgaXMgbm8gbW9yZSB0aGFuIGhhbGYgYSB2aWV3cG9ydCBoZWlnaHQgYXdheSBmcm9tIHRoZVxuICogICBib3R0b20gb2YgdGhlIGlucHV0XG4gKiAtIFRoZSBmaXJzdCBgPGxhYmVsPmAgdGhhdCBpcyBhc3NvY2lhdGVkIHdpdGggdGhlIGlucHV0IHVzaW5nIGZvcj1cImlucHV0SWRcIlxuICogLSBUaGUgY2xvc2VzdCBwYXJlbnQgYDxsYWJlbD5gXG4gKlxuICogQHBhcmFtIHtIVE1MRWxlbWVudH0gJGlucHV0IC0gVGhlIGlucHV0XG4gKiBAcmV0dXJucyB7SFRNTEVsZW1lbnR9IEFzc29jaWF0ZWQgbGVnZW5kIG9yIGxhYmVsLCBvciBudWxsIGlmIG5vIGFzc29jaWF0ZWRcbiAqICAgICAgICAgICAgICAgICAgICAgICAgbGVnZW5kIG9yIGxhYmVsIGNhbiBiZSBmb3VuZFxuICovXG5FcnJvclN1bW1hcnkucHJvdG90eXBlLmdldEFzc29jaWF0ZWRMZWdlbmRPckxhYmVsID0gZnVuY3Rpb24gKCRpbnB1dCkge1xuICB2YXIgJGZpZWxkc2V0ID0gJGlucHV0LmNsb3Nlc3QoJ2ZpZWxkc2V0Jyk7XG5cbiAgaWYgKCRmaWVsZHNldCkge1xuICAgIHZhciBsZWdlbmRzID0gJGZpZWxkc2V0LmdldEVsZW1lbnRzQnlUYWdOYW1lKCdsZWdlbmQnKTtcblxuICAgIGlmIChsZWdlbmRzLmxlbmd0aCkge1xuICAgICAgdmFyICRjYW5kaWRhdGVMZWdlbmQgPSBsZWdlbmRzWzBdO1xuXG4gICAgICAvLyBJZiB0aGUgaW5wdXQgdHlwZSBpcyByYWRpbyBvciBjaGVja2JveCwgYWx3YXlzIHVzZSB0aGUgbGVnZW5kIGlmIHRoZXJlXG4gICAgICAvLyBpcyBvbmUuXG4gICAgICBpZiAoJGlucHV0LnR5cGUgPT09ICdjaGVja2JveCcgfHwgJGlucHV0LnR5cGUgPT09ICdyYWRpbycpIHtcbiAgICAgICAgcmV0dXJuICRjYW5kaWRhdGVMZWdlbmRcbiAgICAgIH1cblxuICAgICAgLy8gRm9yIG90aGVyIGlucHV0IHR5cGVzLCBvbmx5IHNjcm9sbCB0byB0aGUgZmllbGRzZXTigJlzIGxlZ2VuZCAoaW5zdGVhZCBvZlxuICAgICAgLy8gdGhlIGxhYmVsIGFzc29jaWF0ZWQgd2l0aCB0aGUgaW5wdXQpIGlmIHRoZSBpbnB1dCB3b3VsZCBlbmQgdXAgaW4gdGhlXG4gICAgICAvLyB0b3AgaGFsZiBvZiB0aGUgc2NyZWVuLlxuICAgICAgLy9cbiAgICAgIC8vIFRoaXMgc2hvdWxkIGF2b2lkIHNpdHVhdGlvbnMgd2hlcmUgdGhlIGlucHV0IGVpdGhlciBlbmRzIHVwIG9mZiB0aGVcbiAgICAgIC8vIHNjcmVlbiwgb3Igb2JzY3VyZWQgYnkgYSBzb2Z0d2FyZSBrZXlib2FyZC5cbiAgICAgIHZhciBsZWdlbmRUb3AgPSAkY2FuZGlkYXRlTGVnZW5kLmdldEJvdW5kaW5nQ2xpZW50UmVjdCgpLnRvcDtcbiAgICAgIHZhciBpbnB1dFJlY3QgPSAkaW5wdXQuZ2V0Qm91bmRpbmdDbGllbnRSZWN0KCk7XG5cbiAgICAgIC8vIElmIHRoZSBicm93c2VyIGRvZXNuJ3Qgc3VwcG9ydCBFbGVtZW50LmdldEJvdW5kaW5nQ2xpZW50UmVjdCgpLmhlaWdodFxuICAgICAgLy8gb3Igd2luZG93LmlubmVySGVpZ2h0IChsaWtlIElFOCksIGJhaWwgYW5kIGp1c3QgbGluayB0byB0aGUgbGFiZWwuXG4gICAgICBpZiAoaW5wdXRSZWN0LmhlaWdodCAmJiB3aW5kb3cuaW5uZXJIZWlnaHQpIHtcbiAgICAgICAgdmFyIGlucHV0Qm90dG9tID0gaW5wdXRSZWN0LnRvcCArIGlucHV0UmVjdC5oZWlnaHQ7XG5cbiAgICAgICAgaWYgKGlucHV0Qm90dG9tIC0gbGVnZW5kVG9wIDwgd2luZG93LmlubmVySGVpZ2h0IC8gMikge1xuICAgICAgICAgIHJldHVybiAkY2FuZGlkYXRlTGVnZW5kXG4gICAgICAgIH1cbiAgICAgIH1cbiAgICB9XG4gIH1cblxuICByZXR1cm4gZG9jdW1lbnQucXVlcnlTZWxlY3RvcihcImxhYmVsW2Zvcj0nXCIgKyAkaW5wdXQuZ2V0QXR0cmlidXRlKCdpZCcpICsgXCInXVwiKSB8fFxuICAgICRpbnB1dC5jbG9zZXN0KCdsYWJlbCcpXG59O1xuXG5mdW5jdGlvbiBIZWFkZXIgKCRtb2R1bGUpIHtcbiAgdGhpcy4kbW9kdWxlID0gJG1vZHVsZTtcbn1cblxuSGVhZGVyLnByb3RvdHlwZS5pbml0ID0gZnVuY3Rpb24gKCkge1xuICAvLyBDaGVjayBmb3IgbW9kdWxlXG4gIHZhciAkbW9kdWxlID0gdGhpcy4kbW9kdWxlO1xuICBpZiAoISRtb2R1bGUpIHtcbiAgICByZXR1cm5cbiAgfVxuXG4gIC8vIENoZWNrIGZvciBidXR0b25cbiAgdmFyICR0b2dnbGVCdXR0b24gPSAkbW9kdWxlLnF1ZXJ5U2VsZWN0b3IoJy5nb3Z1ay1qcy1oZWFkZXItdG9nZ2xlJyk7XG4gIGlmICghJHRvZ2dsZUJ1dHRvbikge1xuICAgIHJldHVyblxuICB9XG5cbiAgLy8gSGFuZGxlICR0b2dnbGVCdXR0b24gY2xpY2sgZXZlbnRzXG4gICR0b2dnbGVCdXR0b24uYWRkRXZlbnRMaXN0ZW5lcignY2xpY2snLCB0aGlzLmhhbmRsZUNsaWNrLmJpbmQodGhpcykpO1xufTtcblxuLyoqXG4qIFRvZ2dsZSBjbGFzc1xuKiBAcGFyYW0ge29iamVjdH0gbm9kZSBlbGVtZW50XG4qIEBwYXJhbSB7c3RyaW5nfSBjbGFzc05hbWUgdG8gdG9nZ2xlXG4qL1xuSGVhZGVyLnByb3RvdHlwZS50b2dnbGVDbGFzcyA9IGZ1bmN0aW9uIChub2RlLCBjbGFzc05hbWUpIHtcbiAgaWYgKG5vZGUuY2xhc3NOYW1lLmluZGV4T2YoY2xhc3NOYW1lKSA+IDApIHtcbiAgICBub2RlLmNsYXNzTmFtZSA9IG5vZGUuY2xhc3NOYW1lLnJlcGxhY2UoJyAnICsgY2xhc3NOYW1lLCAnJyk7XG4gIH0gZWxzZSB7XG4gICAgbm9kZS5jbGFzc05hbWUgKz0gJyAnICsgY2xhc3NOYW1lO1xuICB9XG59O1xuXG4vKipcbiogQW4gZXZlbnQgaGFuZGxlciBmb3IgY2xpY2sgZXZlbnQgb24gJHRvZ2dsZUJ1dHRvblxuKiBAcGFyYW0ge29iamVjdH0gZXZlbnQgZXZlbnRcbiovXG5IZWFkZXIucHJvdG90eXBlLmhhbmRsZUNsaWNrID0gZnVuY3Rpb24gKGV2ZW50KSB7XG4gIHZhciAkbW9kdWxlID0gdGhpcy4kbW9kdWxlO1xuICB2YXIgJHRvZ2dsZUJ1dHRvbiA9IGV2ZW50LnRhcmdldCB8fCBldmVudC5zcmNFbGVtZW50O1xuICB2YXIgJHRhcmdldCA9ICRtb2R1bGUucXVlcnlTZWxlY3RvcignIycgKyAkdG9nZ2xlQnV0dG9uLmdldEF0dHJpYnV0ZSgnYXJpYS1jb250cm9scycpKTtcblxuICAvLyBJZiBhIGJ1dHRvbiB3aXRoIGFyaWEtY29udHJvbHMsIGhhbmRsZSBjbGlja1xuICBpZiAoJHRvZ2dsZUJ1dHRvbiAmJiAkdGFyZ2V0KSB7XG4gICAgdGhpcy50b2dnbGVDbGFzcygkdGFyZ2V0LCAnZ292dWstaGVhZGVyX19uYXZpZ2F0aW9uLS1vcGVuJyk7XG4gICAgdGhpcy50b2dnbGVDbGFzcygkdG9nZ2xlQnV0dG9uLCAnZ292dWstaGVhZGVyX19tZW51LWJ1dHRvbi0tb3BlbicpO1xuXG4gICAgJHRvZ2dsZUJ1dHRvbi5zZXRBdHRyaWJ1dGUoJ2FyaWEtZXhwYW5kZWQnLCAkdG9nZ2xlQnV0dG9uLmdldEF0dHJpYnV0ZSgnYXJpYS1leHBhbmRlZCcpICE9PSAndHJ1ZScpO1xuICAgICR0YXJnZXQuc2V0QXR0cmlidXRlKCdhcmlhLWhpZGRlbicsICR0YXJnZXQuZ2V0QXR0cmlidXRlKCdhcmlhLWhpZGRlbicpID09PSAnZmFsc2UnKTtcbiAgfVxufTtcblxuZnVuY3Rpb24gUmFkaW9zICgkbW9kdWxlKSB7XG4gIHRoaXMuJG1vZHVsZSA9ICRtb2R1bGU7XG59XG5cblJhZGlvcy5wcm90b3R5cGUuaW5pdCA9IGZ1bmN0aW9uICgpIHtcbiAgdmFyICRtb2R1bGUgPSB0aGlzLiRtb2R1bGU7XG4gIHZhciAkaW5wdXRzID0gJG1vZHVsZS5xdWVyeVNlbGVjdG9yQWxsKCdpbnB1dFt0eXBlPVwicmFkaW9cIl0nKTtcblxuICAvKipcbiAgKiBMb29wIG92ZXIgYWxsIGl0ZW1zIHdpdGggW2RhdGEtY29udHJvbHNdXG4gICogQ2hlY2sgaWYgdGhleSBoYXZlIGEgbWF0Y2hpbmcgY29uZGl0aW9uYWwgcmV2ZWFsXG4gICogSWYgdGhleSBkbywgYXNzaWduIGF0dHJpYnV0ZXMuXG4gICoqL1xuICBub2RlTGlzdEZvckVhY2goJGlucHV0cywgZnVuY3Rpb24gKCRpbnB1dCkge1xuICAgIHZhciBjb250cm9scyA9ICRpbnB1dC5nZXRBdHRyaWJ1dGUoJ2RhdGEtYXJpYS1jb250cm9scycpO1xuXG4gICAgLy8gQ2hlY2sgaWYgaW5wdXQgY29udHJvbHMgYW55dGhpbmdcbiAgICAvLyBDaGVjayBpZiBjb250ZW50IGV4aXN0cywgYmVmb3JlIHNldHRpbmcgYXR0cmlidXRlcy5cbiAgICBpZiAoIWNvbnRyb2xzIHx8ICEkbW9kdWxlLnF1ZXJ5U2VsZWN0b3IoJyMnICsgY29udHJvbHMpKSB7XG4gICAgICByZXR1cm5cbiAgICB9XG5cbiAgICAvLyBJZiB3ZSBoYXZlIGNvbnRlbnQgdGhhdCBpcyBjb250cm9sbGVkLCBzZXQgYXR0cmlidXRlcy5cbiAgICAkaW5wdXQuc2V0QXR0cmlidXRlKCdhcmlhLWNvbnRyb2xzJywgY29udHJvbHMpO1xuICAgICRpbnB1dC5yZW1vdmVBdHRyaWJ1dGUoJ2RhdGEtYXJpYS1jb250cm9scycpO1xuICAgIHRoaXMuc2V0QXR0cmlidXRlcygkaW5wdXQpO1xuICB9LmJpbmQodGhpcykpO1xuXG4gIC8vIEhhbmRsZSBldmVudHNcbiAgJG1vZHVsZS5hZGRFdmVudExpc3RlbmVyKCdjbGljaycsIHRoaXMuaGFuZGxlQ2xpY2suYmluZCh0aGlzKSk7XG59O1xuXG5SYWRpb3MucHJvdG90eXBlLnNldEF0dHJpYnV0ZXMgPSBmdW5jdGlvbiAoJGlucHV0KSB7XG4gIHZhciAkY29udGVudCA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJyMnICsgJGlucHV0LmdldEF0dHJpYnV0ZSgnYXJpYS1jb250cm9scycpKTtcblxuICBpZiAoJGNvbnRlbnQgJiYgJGNvbnRlbnQuY2xhc3NMaXN0LmNvbnRhaW5zKCdnb3Z1ay1yYWRpb3NfX2NvbmRpdGlvbmFsJykpIHtcbiAgICB2YXIgaW5wdXRJc0NoZWNrZWQgPSAkaW5wdXQuY2hlY2tlZDtcblxuICAgICRpbnB1dC5zZXRBdHRyaWJ1dGUoJ2FyaWEtZXhwYW5kZWQnLCBpbnB1dElzQ2hlY2tlZCk7XG5cbiAgICAkY29udGVudC5jbGFzc0xpc3QudG9nZ2xlKCdnb3Z1ay1yYWRpb3NfX2NvbmRpdGlvbmFsLS1oaWRkZW4nLCAhaW5wdXRJc0NoZWNrZWQpO1xuICB9XG59O1xuXG5SYWRpb3MucHJvdG90eXBlLmhhbmRsZUNsaWNrID0gZnVuY3Rpb24gKGV2ZW50KSB7XG4gIHZhciAkY2xpY2tlZElucHV0ID0gZXZlbnQudGFyZ2V0O1xuICAvLyBXZSBvbmx5IHdhbnQgdG8gaGFuZGxlIGNsaWNrcyBmb3IgcmFkaW8gaW5wdXRzXG4gIGlmICgkY2xpY2tlZElucHV0LnR5cGUgIT09ICdyYWRpbycpIHtcbiAgICByZXR1cm5cbiAgfVxuICAvLyBCZWNhdXNlIGNoZWNraW5nIG9uZSByYWRpbyBjYW4gdW5jaGVjayBhIHJhZGlvIGluIGFub3RoZXIgJG1vZHVsZSxcbiAgLy8gd2UgbmVlZCB0byBjYWxsIHNldCBhdHRyaWJ1dGVzIG9uIGFsbCByYWRpb3MgaW4gdGhlIHNhbWUgZm9ybSwgb3IgZG9jdW1lbnQgaWYgdGhleSdyZSBub3QgaW4gYSBmb3JtLlxuICAvL1xuICAvLyBXZSBhbHNvIG9ubHkgd2FudCByYWRpb3Mgd2hpY2ggaGF2ZSBhcmlhLWNvbnRyb2xzLCBhcyB0aGV5IHN1cHBvcnQgY29uZGl0aW9uYWwgcmV2ZWFscy5cbiAgdmFyICRhbGxJbnB1dHMgPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yQWxsKCdpbnB1dFt0eXBlPVwicmFkaW9cIl1bYXJpYS1jb250cm9sc10nKTtcbiAgbm9kZUxpc3RGb3JFYWNoKCRhbGxJbnB1dHMsIGZ1bmN0aW9uICgkaW5wdXQpIHtcbiAgICAvLyBPbmx5IGlucHV0cyB3aXRoIHRoZSBzYW1lIGZvcm0gb3duZXIgc2hvdWxkIGNoYW5nZS5cbiAgICB2YXIgaGFzU2FtZUZvcm1Pd25lciA9ICgkaW5wdXQuZm9ybSA9PT0gJGNsaWNrZWRJbnB1dC5mb3JtKTtcblxuICAgIC8vIEluIHJhZGlvcywgb25seSByYWRpb3Mgd2l0aCB0aGUgc2FtZSBuYW1lIHdpbGwgYWZmZWN0IGVhY2ggb3RoZXIuXG4gICAgdmFyIGhhc1NhbWVOYW1lID0gKCRpbnB1dC5uYW1lID09PSAkY2xpY2tlZElucHV0Lm5hbWUpO1xuICAgIGlmIChoYXNTYW1lTmFtZSAmJiBoYXNTYW1lRm9ybU93bmVyKSB7XG4gICAgICB0aGlzLnNldEF0dHJpYnV0ZXMoJGlucHV0KTtcbiAgICB9XG4gIH0uYmluZCh0aGlzKSk7XG59O1xuXG4oZnVuY3Rpb24odW5kZWZpbmVkKSB7XG5cbiAgICAvLyBEZXRlY3Rpb24gZnJvbSBodHRwczovL2dpdGh1Yi5jb20vRmluYW5jaWFsLVRpbWVzL3BvbHlmaWxsLXNlcnZpY2UvcHVsbC8xMDYyL2ZpbGVzI2RpZmYtYjA5YTVkMmFjZjMzMTRiNDZhNmM4ZjhkMGMzMWI4NWNcbiAgICB2YXIgZGV0ZWN0ID0gKFxuICAgICAgJ0VsZW1lbnQnIGluIHRoaXMgJiYgXCJuZXh0RWxlbWVudFNpYmxpbmdcIiBpbiBkb2N1bWVudC5kb2N1bWVudEVsZW1lbnRcbiAgICApO1xuXG4gICAgaWYgKGRldGVjdCkgcmV0dXJuXG5cblxuICAgIChmdW5jdGlvbiAoZ2xvYmFsKSB7XG5cbiAgICAgIC8vIFBvbHlmaWxsIGZyb20gaHR0cHM6Ly9naXRodWIuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlL3B1bGwvMTA2Mi9maWxlcyNkaWZmLTQwNGI2OWI0NzUwZDE4ZGVhNDE3NDkzMGE0OTE3MGZkXG4gICAgICBPYmplY3QuZGVmaW5lUHJvcGVydHkoRWxlbWVudC5wcm90b3R5cGUsIFwibmV4dEVsZW1lbnRTaWJsaW5nXCIsIHtcbiAgICAgICAgZ2V0OiBmdW5jdGlvbigpe1xuICAgICAgICAgIHZhciBlbCA9IHRoaXMubmV4dFNpYmxpbmc7XG4gICAgICAgICAgd2hpbGUgKGVsICYmIGVsLm5vZGVUeXBlICE9PSAxKSB7IGVsID0gZWwubmV4dFNpYmxpbmc7IH1cbiAgICAgICAgICByZXR1cm4gKGVsLm5vZGVUeXBlID09PSAxKSA/IGVsIDogbnVsbDtcbiAgICAgICAgfVxuICAgICAgfSk7XG5cbiAgICB9KHRoaXMpKTtcblxufSkuY2FsbCgnb2JqZWN0JyA9PT0gdHlwZW9mIHdpbmRvdyAmJiB3aW5kb3cgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBzZWxmICYmIHNlbGYgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBnbG9iYWwgJiYgZ2xvYmFsIHx8IHt9KTtcblxuKGZ1bmN0aW9uKHVuZGVmaW5lZCkge1xuXG4gICAgLy8gRGV0ZWN0aW9uIGZyb20gaHR0cHM6Ly9naXRodWIuY29tL0ZpbmFuY2lhbC1UaW1lcy9wb2x5ZmlsbC1zZXJ2aWNlL3B1bGwvMTA2Mi9maWxlcyNkaWZmLWExNjIyMzVmYmM5YzBkZDQwZDQwMzIyNjVmNDQ5NDJlXG4gICAgdmFyIGRldGVjdCA9IChcbiAgICAgICdFbGVtZW50JyBpbiB0aGlzICYmICdwcmV2aW91c0VsZW1lbnRTaWJsaW5nJyBpbiBkb2N1bWVudC5kb2N1bWVudEVsZW1lbnRcbiAgICApO1xuXG4gICAgaWYgKGRldGVjdCkgcmV0dXJuXG5cbiAgICAoZnVuY3Rpb24gKGdsb2JhbCkge1xuICAgICAgLy8gUG9seWZpbGwgZnJvbSBodHRwczovL2dpdGh1Yi5jb20vRmluYW5jaWFsLVRpbWVzL3BvbHlmaWxsLXNlcnZpY2UvcHVsbC8xMDYyL2ZpbGVzI2RpZmYtYjQ1YTExOTdiODQyNzI4Y2I3NmI2MjRiNmJhN2Q3MzlcbiAgICAgIE9iamVjdC5kZWZpbmVQcm9wZXJ0eShFbGVtZW50LnByb3RvdHlwZSwgJ3ByZXZpb3VzRWxlbWVudFNpYmxpbmcnLCB7XG4gICAgICAgIGdldDogZnVuY3Rpb24oKXtcbiAgICAgICAgICB2YXIgZWwgPSB0aGlzLnByZXZpb3VzU2libGluZztcbiAgICAgICAgICB3aGlsZSAoZWwgJiYgZWwubm9kZVR5cGUgIT09IDEpIHsgZWwgPSBlbC5wcmV2aW91c1NpYmxpbmc7IH1cbiAgICAgICAgICByZXR1cm4gKGVsLm5vZGVUeXBlID09PSAxKSA/IGVsIDogbnVsbDtcbiAgICAgICAgfVxuICAgICAgfSk7XG5cbiAgICB9KHRoaXMpKTtcblxufSkuY2FsbCgnb2JqZWN0JyA9PT0gdHlwZW9mIHdpbmRvdyAmJiB3aW5kb3cgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBzZWxmICYmIHNlbGYgfHwgJ29iamVjdCcgPT09IHR5cGVvZiBnbG9iYWwgJiYgZ2xvYmFsIHx8IHt9KTtcblxuZnVuY3Rpb24gVGFicyAoJG1vZHVsZSkge1xuICB0aGlzLiRtb2R1bGUgPSAkbW9kdWxlO1xuICB0aGlzLiR0YWJzID0gJG1vZHVsZS5xdWVyeVNlbGVjdG9yQWxsKCcuZ292dWstdGFic19fdGFiJyk7XG5cbiAgdGhpcy5rZXlzID0geyBsZWZ0OiAzNywgcmlnaHQ6IDM5LCB1cDogMzgsIGRvd246IDQwIH07XG4gIHRoaXMuanNIaWRkZW5DbGFzcyA9ICdnb3Z1ay10YWJzX19wYW5lbC0taGlkZGVuJztcbn1cblxuVGFicy5wcm90b3R5cGUuaW5pdCA9IGZ1bmN0aW9uICgpIHtcbiAgaWYgKHR5cGVvZiB3aW5kb3cubWF0Y2hNZWRpYSA9PT0gJ2Z1bmN0aW9uJykge1xuICAgIHRoaXMuc2V0dXBSZXNwb25zaXZlQ2hlY2tzKCk7XG4gIH0gZWxzZSB7XG4gICAgdGhpcy5zZXR1cCgpO1xuICB9XG59O1xuXG5UYWJzLnByb3RvdHlwZS5zZXR1cFJlc3BvbnNpdmVDaGVja3MgPSBmdW5jdGlvbiAoKSB7XG4gIHRoaXMubXFsID0gd2luZG93Lm1hdGNoTWVkaWEoJyhtaW4td2lkdGg6IDQwLjA2MjVlbSknKTtcbiAgdGhpcy5tcWwuYWRkTGlzdGVuZXIodGhpcy5jaGVja01vZGUuYmluZCh0aGlzKSk7XG4gIHRoaXMuY2hlY2tNb2RlKCk7XG59O1xuXG5UYWJzLnByb3RvdHlwZS5jaGVja01vZGUgPSBmdW5jdGlvbiAoKSB7XG4gIGlmICh0aGlzLm1xbC5tYXRjaGVzKSB7XG4gICAgdGhpcy5zZXR1cCgpO1xuICB9IGVsc2Uge1xuICAgIHRoaXMudGVhcmRvd24oKTtcbiAgfVxufTtcblxuVGFicy5wcm90b3R5cGUuc2V0dXAgPSBmdW5jdGlvbiAoKSB7XG4gIHZhciAkbW9kdWxlID0gdGhpcy4kbW9kdWxlO1xuICB2YXIgJHRhYnMgPSB0aGlzLiR0YWJzO1xuICB2YXIgJHRhYkxpc3QgPSAkbW9kdWxlLnF1ZXJ5U2VsZWN0b3IoJy5nb3Z1ay10YWJzX19saXN0Jyk7XG4gIHZhciAkdGFiTGlzdEl0ZW1zID0gJG1vZHVsZS5xdWVyeVNlbGVjdG9yQWxsKCcuZ292dWstdGFic19fbGlzdC1pdGVtJyk7XG5cbiAgaWYgKCEkdGFicyB8fCAhJHRhYkxpc3QgfHwgISR0YWJMaXN0SXRlbXMpIHtcbiAgICByZXR1cm5cbiAgfVxuXG4gICR0YWJMaXN0LnNldEF0dHJpYnV0ZSgncm9sZScsICd0YWJsaXN0Jyk7XG5cbiAgbm9kZUxpc3RGb3JFYWNoKCR0YWJMaXN0SXRlbXMsIGZ1bmN0aW9uICgkaXRlbSkge1xuICAgICRpdGVtLnNldEF0dHJpYnV0ZSgncm9sZScsICdwcmVzZW50YXRpb24nKTtcbiAgfSk7XG5cbiAgbm9kZUxpc3RGb3JFYWNoKCR0YWJzLCBmdW5jdGlvbiAoJHRhYikge1xuICAgIC8vIFNldCBIVE1MIGF0dHJpYnV0ZXNcbiAgICB0aGlzLnNldEF0dHJpYnV0ZXMoJHRhYik7XG5cbiAgICAvLyBTYXZlIGJvdW5kZWQgZnVuY3Rpb25zIHRvIHVzZSB3aGVuIHJlbW92aW5nIGV2ZW50IGxpc3RlbmVycyBkdXJpbmcgdGVhcmRvd25cbiAgICAkdGFiLmJvdW5kVGFiQ2xpY2sgPSB0aGlzLm9uVGFiQ2xpY2suYmluZCh0aGlzKTtcbiAgICAkdGFiLmJvdW5kVGFiS2V5ZG93biA9IHRoaXMub25UYWJLZXlkb3duLmJpbmQodGhpcyk7XG5cbiAgICAvLyBIYW5kbGUgZXZlbnRzXG4gICAgJHRhYi5hZGRFdmVudExpc3RlbmVyKCdjbGljaycsICR0YWIuYm91bmRUYWJDbGljaywgdHJ1ZSk7XG4gICAgJHRhYi5hZGRFdmVudExpc3RlbmVyKCdrZXlkb3duJywgJHRhYi5ib3VuZFRhYktleWRvd24sIHRydWUpO1xuXG4gICAgLy8gUmVtb3ZlIG9sZCBhY3RpdmUgcGFuZWxzXG4gICAgdGhpcy5oaWRlVGFiKCR0YWIpO1xuICB9LmJpbmQodGhpcykpO1xuXG4gIC8vIFNob3cgZWl0aGVyIHRoZSBhY3RpdmUgdGFiIGFjY29yZGluZyB0byB0aGUgVVJMJ3MgaGFzaCBvciB0aGUgZmlyc3QgdGFiXG4gIHZhciAkYWN0aXZlVGFiID0gdGhpcy5nZXRUYWIod2luZG93LmxvY2F0aW9uLmhhc2gpIHx8IHRoaXMuJHRhYnNbMF07XG4gIHRoaXMuc2hvd1RhYigkYWN0aXZlVGFiKTtcblxuICAvLyBIYW5kbGUgaGFzaGNoYW5nZSBldmVudHNcbiAgJG1vZHVsZS5ib3VuZE9uSGFzaENoYW5nZSA9IHRoaXMub25IYXNoQ2hhbmdlLmJpbmQodGhpcyk7XG4gIHdpbmRvdy5hZGRFdmVudExpc3RlbmVyKCdoYXNoY2hhbmdlJywgJG1vZHVsZS5ib3VuZE9uSGFzaENoYW5nZSwgdHJ1ZSk7XG59O1xuXG5UYWJzLnByb3RvdHlwZS50ZWFyZG93biA9IGZ1bmN0aW9uICgpIHtcbiAgdmFyICRtb2R1bGUgPSB0aGlzLiRtb2R1bGU7XG4gIHZhciAkdGFicyA9IHRoaXMuJHRhYnM7XG4gIHZhciAkdGFiTGlzdCA9ICRtb2R1bGUucXVlcnlTZWxlY3RvcignLmdvdnVrLXRhYnNfX2xpc3QnKTtcbiAgdmFyICR0YWJMaXN0SXRlbXMgPSAkbW9kdWxlLnF1ZXJ5U2VsZWN0b3JBbGwoJy5nb3Z1ay10YWJzX19saXN0LWl0ZW0nKTtcblxuICBpZiAoISR0YWJzIHx8ICEkdGFiTGlzdCB8fCAhJHRhYkxpc3RJdGVtcykge1xuICAgIHJldHVyblxuICB9XG5cbiAgJHRhYkxpc3QucmVtb3ZlQXR0cmlidXRlKCdyb2xlJyk7XG5cbiAgbm9kZUxpc3RGb3JFYWNoKCR0YWJMaXN0SXRlbXMsIGZ1bmN0aW9uICgkaXRlbSkge1xuICAgICRpdGVtLnJlbW92ZUF0dHJpYnV0ZSgncm9sZScsICdwcmVzZW50YXRpb24nKTtcbiAgfSk7XG5cbiAgbm9kZUxpc3RGb3JFYWNoKCR0YWJzLCBmdW5jdGlvbiAoJHRhYikge1xuICAgIC8vIFJlbW92ZSBldmVudHNcbiAgICAkdGFiLnJlbW92ZUV2ZW50TGlzdGVuZXIoJ2NsaWNrJywgJHRhYi5ib3VuZFRhYkNsaWNrLCB0cnVlKTtcbiAgICAkdGFiLnJlbW92ZUV2ZW50TGlzdGVuZXIoJ2tleWRvd24nLCAkdGFiLmJvdW5kVGFiS2V5ZG93biwgdHJ1ZSk7XG5cbiAgICAvLyBVbnNldCBIVE1MIGF0dHJpYnV0ZXNcbiAgICB0aGlzLnVuc2V0QXR0cmlidXRlcygkdGFiKTtcbiAgfS5iaW5kKHRoaXMpKTtcblxuICAvLyBSZW1vdmUgaGFzaGNoYW5nZSBldmVudCBoYW5kbGVyXG4gIHdpbmRvdy5yZW1vdmVFdmVudExpc3RlbmVyKCdoYXNoY2hhbmdlJywgJG1vZHVsZS5ib3VuZE9uSGFzaENoYW5nZSwgdHJ1ZSk7XG59O1xuXG5UYWJzLnByb3RvdHlwZS5vbkhhc2hDaGFuZ2UgPSBmdW5jdGlvbiAoZSkge1xuICB2YXIgaGFzaCA9IHdpbmRvdy5sb2NhdGlvbi5oYXNoO1xuICB2YXIgJHRhYldpdGhIYXNoID0gdGhpcy5nZXRUYWIoaGFzaCk7XG4gIGlmICghJHRhYldpdGhIYXNoKSB7XG4gICAgcmV0dXJuXG4gIH1cblxuICAvLyBQcmV2ZW50IGNoYW5naW5nIHRoZSBoYXNoXG4gIGlmICh0aGlzLmNoYW5naW5nSGFzaCkge1xuICAgIHRoaXMuY2hhbmdpbmdIYXNoID0gZmFsc2U7XG4gICAgcmV0dXJuXG4gIH1cblxuICAvLyBTaG93IGVpdGhlciB0aGUgYWN0aXZlIHRhYiBhY2NvcmRpbmcgdG8gdGhlIFVSTCdzIGhhc2ggb3IgdGhlIGZpcnN0IHRhYlxuICB2YXIgJHByZXZpb3VzVGFiID0gdGhpcy5nZXRDdXJyZW50VGFiKCk7XG5cbiAgdGhpcy5oaWRlVGFiKCRwcmV2aW91c1RhYik7XG4gIHRoaXMuc2hvd1RhYigkdGFiV2l0aEhhc2gpO1xuICAkdGFiV2l0aEhhc2guZm9jdXMoKTtcbn07XG5cblRhYnMucHJvdG90eXBlLmhpZGVUYWIgPSBmdW5jdGlvbiAoJHRhYikge1xuICB0aGlzLnVuaGlnaGxpZ2h0VGFiKCR0YWIpO1xuICB0aGlzLmhpZGVQYW5lbCgkdGFiKTtcbn07XG5cblRhYnMucHJvdG90eXBlLnNob3dUYWIgPSBmdW5jdGlvbiAoJHRhYikge1xuICB0aGlzLmhpZ2hsaWdodFRhYigkdGFiKTtcbiAgdGhpcy5zaG93UGFuZWwoJHRhYik7XG59O1xuXG5UYWJzLnByb3RvdHlwZS5nZXRUYWIgPSBmdW5jdGlvbiAoaGFzaCkge1xuICByZXR1cm4gdGhpcy4kbW9kdWxlLnF1ZXJ5U2VsZWN0b3IoJy5nb3Z1ay10YWJzX190YWJbaHJlZj1cIicgKyBoYXNoICsgJ1wiXScpXG59O1xuXG5UYWJzLnByb3RvdHlwZS5zZXRBdHRyaWJ1dGVzID0gZnVuY3Rpb24gKCR0YWIpIHtcbiAgLy8gc2V0IHRhYiBhdHRyaWJ1dGVzXG4gIHZhciBwYW5lbElkID0gdGhpcy5nZXRIcmVmKCR0YWIpLnNsaWNlKDEpO1xuICAkdGFiLnNldEF0dHJpYnV0ZSgnaWQnLCAndGFiXycgKyBwYW5lbElkKTtcbiAgJHRhYi5zZXRBdHRyaWJ1dGUoJ3JvbGUnLCAndGFiJyk7XG4gICR0YWIuc2V0QXR0cmlidXRlKCdhcmlhLWNvbnRyb2xzJywgcGFuZWxJZCk7XG4gICR0YWIuc2V0QXR0cmlidXRlKCdhcmlhLXNlbGVjdGVkJywgJ2ZhbHNlJyk7XG4gICR0YWIuc2V0QXR0cmlidXRlKCd0YWJpbmRleCcsICctMScpO1xuXG4gIC8vIHNldCBwYW5lbCBhdHRyaWJ1dGVzXG4gIHZhciAkcGFuZWwgPSB0aGlzLmdldFBhbmVsKCR0YWIpO1xuICAkcGFuZWwuc2V0QXR0cmlidXRlKCdyb2xlJywgJ3RhYnBhbmVsJyk7XG4gICRwYW5lbC5zZXRBdHRyaWJ1dGUoJ2FyaWEtbGFiZWxsZWRieScsICR0YWIuaWQpO1xuICAkcGFuZWwuY2xhc3NMaXN0LmFkZCh0aGlzLmpzSGlkZGVuQ2xhc3MpO1xufTtcblxuVGFicy5wcm90b3R5cGUudW5zZXRBdHRyaWJ1dGVzID0gZnVuY3Rpb24gKCR0YWIpIHtcbiAgLy8gdW5zZXQgdGFiIGF0dHJpYnV0ZXNcbiAgJHRhYi5yZW1vdmVBdHRyaWJ1dGUoJ2lkJyk7XG4gICR0YWIucmVtb3ZlQXR0cmlidXRlKCdyb2xlJyk7XG4gICR0YWIucmVtb3ZlQXR0cmlidXRlKCdhcmlhLWNvbnRyb2xzJyk7XG4gICR0YWIucmVtb3ZlQXR0cmlidXRlKCdhcmlhLXNlbGVjdGVkJyk7XG4gICR0YWIucmVtb3ZlQXR0cmlidXRlKCd0YWJpbmRleCcpO1xuXG4gIC8vIHVuc2V0IHBhbmVsIGF0dHJpYnV0ZXNcbiAgdmFyICRwYW5lbCA9IHRoaXMuZ2V0UGFuZWwoJHRhYik7XG4gICRwYW5lbC5yZW1vdmVBdHRyaWJ1dGUoJ3JvbGUnKTtcbiAgJHBhbmVsLnJlbW92ZUF0dHJpYnV0ZSgnYXJpYS1sYWJlbGxlZGJ5Jyk7XG4gICRwYW5lbC5jbGFzc0xpc3QucmVtb3ZlKHRoaXMuanNIaWRkZW5DbGFzcyk7XG59O1xuXG5UYWJzLnByb3RvdHlwZS5vblRhYkNsaWNrID0gZnVuY3Rpb24gKGUpIHtcbiAgaWYgKCFlLnRhcmdldC5jbGFzc0xpc3QuY29udGFpbnMoJ2dvdnVrLXRhYnNfX3RhYicpKSB7XG4gIC8vIEFsbG93IGV2ZW50cyBvbiBjaGlsZCBET00gZWxlbWVudHMgdG8gYnViYmxlIHVwIHRvIHRhYiBwYXJlbnRcbiAgICByZXR1cm4gZmFsc2VcbiAgfVxuICBlLnByZXZlbnREZWZhdWx0KCk7XG4gIHZhciAkbmV3VGFiID0gZS50YXJnZXQ7XG4gIHZhciAkY3VycmVudFRhYiA9IHRoaXMuZ2V0Q3VycmVudFRhYigpO1xuICB0aGlzLmhpZGVUYWIoJGN1cnJlbnRUYWIpO1xuICB0aGlzLnNob3dUYWIoJG5ld1RhYik7XG4gIHRoaXMuY3JlYXRlSGlzdG9yeUVudHJ5KCRuZXdUYWIpO1xufTtcblxuVGFicy5wcm90b3R5cGUuY3JlYXRlSGlzdG9yeUVudHJ5ID0gZnVuY3Rpb24gKCR0YWIpIHtcbiAgdmFyICRwYW5lbCA9IHRoaXMuZ2V0UGFuZWwoJHRhYik7XG5cbiAgLy8gU2F2ZSBhbmQgcmVzdG9yZSB0aGUgaWRcbiAgLy8gc28gdGhlIHBhZ2UgZG9lc24ndCBqdW1wIHdoZW4gYSB1c2VyIGNsaWNrcyBhIHRhYiAod2hpY2ggY2hhbmdlcyB0aGUgaGFzaClcbiAgdmFyIGlkID0gJHBhbmVsLmlkO1xuICAkcGFuZWwuaWQgPSAnJztcbiAgdGhpcy5jaGFuZ2luZ0hhc2ggPSB0cnVlO1xuICB3aW5kb3cubG9jYXRpb24uaGFzaCA9IHRoaXMuZ2V0SHJlZigkdGFiKS5zbGljZSgxKTtcbiAgJHBhbmVsLmlkID0gaWQ7XG59O1xuXG5UYWJzLnByb3RvdHlwZS5vblRhYktleWRvd24gPSBmdW5jdGlvbiAoZSkge1xuICBzd2l0Y2ggKGUua2V5Q29kZSkge1xuICAgIGNhc2UgdGhpcy5rZXlzLmxlZnQ6XG4gICAgY2FzZSB0aGlzLmtleXMudXA6XG4gICAgICB0aGlzLmFjdGl2YXRlUHJldmlvdXNUYWIoKTtcbiAgICAgIGUucHJldmVudERlZmF1bHQoKTtcbiAgICAgIGJyZWFrXG4gICAgY2FzZSB0aGlzLmtleXMucmlnaHQ6XG4gICAgY2FzZSB0aGlzLmtleXMuZG93bjpcbiAgICAgIHRoaXMuYWN0aXZhdGVOZXh0VGFiKCk7XG4gICAgICBlLnByZXZlbnREZWZhdWx0KCk7XG4gICAgICBicmVha1xuICB9XG59O1xuXG5UYWJzLnByb3RvdHlwZS5hY3RpdmF0ZU5leHRUYWIgPSBmdW5jdGlvbiAoKSB7XG4gIHZhciBjdXJyZW50VGFiID0gdGhpcy5nZXRDdXJyZW50VGFiKCk7XG4gIHZhciBuZXh0VGFiTGlzdEl0ZW0gPSBjdXJyZW50VGFiLnBhcmVudE5vZGUubmV4dEVsZW1lbnRTaWJsaW5nO1xuICBpZiAobmV4dFRhYkxpc3RJdGVtKSB7XG4gICAgdmFyIG5leHRUYWIgPSBuZXh0VGFiTGlzdEl0ZW0ucXVlcnlTZWxlY3RvcignLmdvdnVrLXRhYnNfX3RhYicpO1xuICB9XG4gIGlmIChuZXh0VGFiKSB7XG4gICAgdGhpcy5oaWRlVGFiKGN1cnJlbnRUYWIpO1xuICAgIHRoaXMuc2hvd1RhYihuZXh0VGFiKTtcbiAgICBuZXh0VGFiLmZvY3VzKCk7XG4gICAgdGhpcy5jcmVhdGVIaXN0b3J5RW50cnkobmV4dFRhYik7XG4gIH1cbn07XG5cblRhYnMucHJvdG90eXBlLmFjdGl2YXRlUHJldmlvdXNUYWIgPSBmdW5jdGlvbiAoKSB7XG4gIHZhciBjdXJyZW50VGFiID0gdGhpcy5nZXRDdXJyZW50VGFiKCk7XG4gIHZhciBwcmV2aW91c1RhYkxpc3RJdGVtID0gY3VycmVudFRhYi5wYXJlbnROb2RlLnByZXZpb3VzRWxlbWVudFNpYmxpbmc7XG4gIGlmIChwcmV2aW91c1RhYkxpc3RJdGVtKSB7XG4gICAgdmFyIHByZXZpb3VzVGFiID0gcHJldmlvdXNUYWJMaXN0SXRlbS5xdWVyeVNlbGVjdG9yKCcuZ292dWstdGFic19fdGFiJyk7XG4gIH1cbiAgaWYgKHByZXZpb3VzVGFiKSB7XG4gICAgdGhpcy5oaWRlVGFiKGN1cnJlbnRUYWIpO1xuICAgIHRoaXMuc2hvd1RhYihwcmV2aW91c1RhYik7XG4gICAgcHJldmlvdXNUYWIuZm9jdXMoKTtcbiAgICB0aGlzLmNyZWF0ZUhpc3RvcnlFbnRyeShwcmV2aW91c1RhYik7XG4gIH1cbn07XG5cblRhYnMucHJvdG90eXBlLmdldFBhbmVsID0gZnVuY3Rpb24gKCR0YWIpIHtcbiAgdmFyICRwYW5lbCA9IHRoaXMuJG1vZHVsZS5xdWVyeVNlbGVjdG9yKHRoaXMuZ2V0SHJlZigkdGFiKSk7XG4gIHJldHVybiAkcGFuZWxcbn07XG5cblRhYnMucHJvdG90eXBlLnNob3dQYW5lbCA9IGZ1bmN0aW9uICgkdGFiKSB7XG4gIHZhciAkcGFuZWwgPSB0aGlzLmdldFBhbmVsKCR0YWIpO1xuICAkcGFuZWwuY2xhc3NMaXN0LnJlbW92ZSh0aGlzLmpzSGlkZGVuQ2xhc3MpO1xufTtcblxuVGFicy5wcm90b3R5cGUuaGlkZVBhbmVsID0gZnVuY3Rpb24gKHRhYikge1xuICB2YXIgJHBhbmVsID0gdGhpcy5nZXRQYW5lbCh0YWIpO1xuICAkcGFuZWwuY2xhc3NMaXN0LmFkZCh0aGlzLmpzSGlkZGVuQ2xhc3MpO1xufTtcblxuVGFicy5wcm90b3R5cGUudW5oaWdobGlnaHRUYWIgPSBmdW5jdGlvbiAoJHRhYikge1xuICAkdGFiLnNldEF0dHJpYnV0ZSgnYXJpYS1zZWxlY3RlZCcsICdmYWxzZScpO1xuICAkdGFiLnBhcmVudE5vZGUuY2xhc3NMaXN0LnJlbW92ZSgnZ292dWstdGFic19fbGlzdC1pdGVtLS1zZWxlY3RlZCcpO1xuICAkdGFiLnNldEF0dHJpYnV0ZSgndGFiaW5kZXgnLCAnLTEnKTtcbn07XG5cblRhYnMucHJvdG90eXBlLmhpZ2hsaWdodFRhYiA9IGZ1bmN0aW9uICgkdGFiKSB7XG4gICR0YWIuc2V0QXR0cmlidXRlKCdhcmlhLXNlbGVjdGVkJywgJ3RydWUnKTtcbiAgJHRhYi5wYXJlbnROb2RlLmNsYXNzTGlzdC5hZGQoJ2dvdnVrLXRhYnNfX2xpc3QtaXRlbS0tc2VsZWN0ZWQnKTtcbiAgJHRhYi5zZXRBdHRyaWJ1dGUoJ3RhYmluZGV4JywgJzAnKTtcbn07XG5cblRhYnMucHJvdG90eXBlLmdldEN1cnJlbnRUYWIgPSBmdW5jdGlvbiAoKSB7XG4gIHJldHVybiB0aGlzLiRtb2R1bGUucXVlcnlTZWxlY3RvcignLmdvdnVrLXRhYnNfX2xpc3QtaXRlbS0tc2VsZWN0ZWQgLmdvdnVrLXRhYnNfX3RhYicpXG59O1xuXG4vLyB0aGlzIGlzIGJlY2F1c2UgSUUgZG9lc24ndCBhbHdheXMgcmV0dXJuIHRoZSBhY3R1YWwgdmFsdWUgYnV0IGEgcmVsYXRpdmUgZnVsbCBwYXRoXG4vLyBzaG91bGQgYmUgYSB1dGlsaXR5IGZ1bmN0aW9uIG1vc3QgcHJvYlxuLy8gaHR0cDovL2xhYnMudGhlc2VkYXlzLmNvbS9ibG9nLzIwMTAvMDEvMDgvZ2V0dGluZy10aGUtaHJlZi12YWx1ZS13aXRoLWpxdWVyeS1pbi1pZS9cblRhYnMucHJvdG90eXBlLmdldEhyZWYgPSBmdW5jdGlvbiAoJHRhYikge1xuICB2YXIgaHJlZiA9ICR0YWIuZ2V0QXR0cmlidXRlKCdocmVmJyk7XG4gIHZhciBoYXNoID0gaHJlZi5zbGljZShocmVmLmluZGV4T2YoJyMnKSwgaHJlZi5sZW5ndGgpO1xuICByZXR1cm4gaGFzaFxufTtcblxuZnVuY3Rpb24gaW5pdEFsbCAob3B0aW9ucykge1xuICAvLyBTZXQgdGhlIG9wdGlvbnMgdG8gYW4gZW1wdHkgb2JqZWN0IGJ5IGRlZmF1bHQgaWYgbm8gb3B0aW9ucyBhcmUgcGFzc2VkLlxuICBvcHRpb25zID0gdHlwZW9mIG9wdGlvbnMgIT09ICd1bmRlZmluZWQnID8gb3B0aW9ucyA6IHt9O1xuXG4gIC8vIEFsbG93IHRoZSB1c2VyIHRvIGluaXRpYWxpc2UgR09WLlVLIEZyb250ZW5kIGluIG9ubHkgY2VydGFpbiBzZWN0aW9ucyBvZiB0aGUgcGFnZVxuICAvLyBEZWZhdWx0cyB0byB0aGUgZW50aXJlIGRvY3VtZW50IGlmIG5vdGhpbmcgaXMgc2V0LlxuICB2YXIgc2NvcGUgPSB0eXBlb2Ygb3B0aW9ucy5zY29wZSAhPT0gJ3VuZGVmaW5lZCcgPyBvcHRpb25zLnNjb3BlIDogZG9jdW1lbnQ7XG5cbiAgdmFyICRidXR0b25zID0gc2NvcGUucXVlcnlTZWxlY3RvckFsbCgnW2RhdGEtbW9kdWxlPVwiZ292dWstYnV0dG9uXCJdJyk7XG4gIG5vZGVMaXN0Rm9yRWFjaCgkYnV0dG9ucywgZnVuY3Rpb24gKCRidXR0b24pIHtcbiAgICBuZXcgQnV0dG9uKCRidXR0b24pLmluaXQoKTtcbiAgfSk7XG5cbiAgdmFyICRhY2NvcmRpb25zID0gc2NvcGUucXVlcnlTZWxlY3RvckFsbCgnW2RhdGEtbW9kdWxlPVwiZ292dWstYWNjb3JkaW9uXCJdJyk7XG4gIG5vZGVMaXN0Rm9yRWFjaCgkYWNjb3JkaW9ucywgZnVuY3Rpb24gKCRhY2NvcmRpb24pIHtcbiAgICBuZXcgQWNjb3JkaW9uKCRhY2NvcmRpb24pLmluaXQoKTtcbiAgfSk7XG5cbiAgdmFyICRkZXRhaWxzID0gc2NvcGUucXVlcnlTZWxlY3RvckFsbCgnW2RhdGEtbW9kdWxlPVwiZ292dWstZGV0YWlsc1wiXScpO1xuICBub2RlTGlzdEZvckVhY2goJGRldGFpbHMsIGZ1bmN0aW9uICgkZGV0YWlsKSB7XG4gICAgbmV3IERldGFpbHMoJGRldGFpbCkuaW5pdCgpO1xuICB9KTtcblxuICB2YXIgJGNoYXJhY3RlckNvdW50cyA9IHNjb3BlLnF1ZXJ5U2VsZWN0b3JBbGwoJ1tkYXRhLW1vZHVsZT1cImdvdnVrLWNoYXJhY3Rlci1jb3VudFwiXScpO1xuICBub2RlTGlzdEZvckVhY2goJGNoYXJhY3RlckNvdW50cywgZnVuY3Rpb24gKCRjaGFyYWN0ZXJDb3VudCkge1xuICAgIG5ldyBDaGFyYWN0ZXJDb3VudCgkY2hhcmFjdGVyQ291bnQpLmluaXQoKTtcbiAgfSk7XG5cbiAgdmFyICRjaGVja2JveGVzID0gc2NvcGUucXVlcnlTZWxlY3RvckFsbCgnW2RhdGEtbW9kdWxlPVwiZ292dWstY2hlY2tib3hlc1wiXScpO1xuICBub2RlTGlzdEZvckVhY2goJGNoZWNrYm94ZXMsIGZ1bmN0aW9uICgkY2hlY2tib3gpIHtcbiAgICBuZXcgQ2hlY2tib3hlcygkY2hlY2tib3gpLmluaXQoKTtcbiAgfSk7XG5cbiAgLy8gRmluZCBmaXJzdCBlcnJvciBzdW1tYXJ5IG1vZHVsZSB0byBlbmhhbmNlLlxuICB2YXIgJGVycm9yU3VtbWFyeSA9IHNjb3BlLnF1ZXJ5U2VsZWN0b3IoJ1tkYXRhLW1vZHVsZT1cImdvdnVrLWVycm9yLXN1bW1hcnlcIl0nKTtcbiAgbmV3IEVycm9yU3VtbWFyeSgkZXJyb3JTdW1tYXJ5KS5pbml0KCk7XG5cbiAgLy8gRmluZCBmaXJzdCBoZWFkZXIgbW9kdWxlIHRvIGVuaGFuY2UuXG4gIHZhciAkdG9nZ2xlQnV0dG9uID0gc2NvcGUucXVlcnlTZWxlY3RvcignW2RhdGEtbW9kdWxlPVwiZ292dWstaGVhZGVyXCJdJyk7XG4gIG5ldyBIZWFkZXIoJHRvZ2dsZUJ1dHRvbikuaW5pdCgpO1xuXG4gIHZhciAkcmFkaW9zID0gc2NvcGUucXVlcnlTZWxlY3RvckFsbCgnW2RhdGEtbW9kdWxlPVwiZ292dWstcmFkaW9zXCJdJyk7XG4gIG5vZGVMaXN0Rm9yRWFjaCgkcmFkaW9zLCBmdW5jdGlvbiAoJHJhZGlvKSB7XG4gICAgbmV3IFJhZGlvcygkcmFkaW8pLmluaXQoKTtcbiAgfSk7XG5cbiAgdmFyICR0YWJzID0gc2NvcGUucXVlcnlTZWxlY3RvckFsbCgnW2RhdGEtbW9kdWxlPVwiZ292dWstdGFic1wiXScpO1xuICBub2RlTGlzdEZvckVhY2goJHRhYnMsIGZ1bmN0aW9uICgkdGFicykge1xuICAgIG5ldyBUYWJzKCR0YWJzKS5pbml0KCk7XG4gIH0pO1xufVxuXG5leHBvcnRzLmluaXRBbGwgPSBpbml0QWxsO1xuZXhwb3J0cy5BY2NvcmRpb24gPSBBY2NvcmRpb247XG5leHBvcnRzLkJ1dHRvbiA9IEJ1dHRvbjtcbmV4cG9ydHMuRGV0YWlscyA9IERldGFpbHM7XG5leHBvcnRzLkNoYXJhY3RlckNvdW50ID0gQ2hhcmFjdGVyQ291bnQ7XG5leHBvcnRzLkNoZWNrYm94ZXMgPSBDaGVja2JveGVzO1xuZXhwb3J0cy5FcnJvclN1bW1hcnkgPSBFcnJvclN1bW1hcnk7XG5leHBvcnRzLkhlYWRlciA9IEhlYWRlcjtcbmV4cG9ydHMuUmFkaW9zID0gUmFkaW9zO1xuZXhwb3J0cy5UYWJzID0gVGFicztcblxufSkpKTtcbiJdfQ==
