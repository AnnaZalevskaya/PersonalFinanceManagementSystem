import "./chunk-J4B6MK7R.js";

// node_modules/@aspnet/signalr/dist/esm/Errors.js
var __extends = function() {
  var extendStatics = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function(d, b) {
    d.__proto__ = b;
  } || function(d, b) {
    for (var p in b)
      if (b.hasOwnProperty(p))
        d[p] = b[p];
  };
  return function(d, b) {
    extendStatics(d, b);
    function __() {
      this.constructor = d;
    }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
  };
}();
var HttpError = (
  /** @class */
  function(_super) {
    __extends(HttpError2, _super);
    function HttpError2(errorMessage, statusCode) {
      var _newTarget = this.constructor;
      var _this = this;
      var trueProto = _newTarget.prototype;
      _this = _super.call(this, errorMessage) || this;
      _this.statusCode = statusCode;
      _this.__proto__ = trueProto;
      return _this;
    }
    return HttpError2;
  }(Error)
);
var TimeoutError = (
  /** @class */
  function(_super) {
    __extends(TimeoutError2, _super);
    function TimeoutError2(errorMessage) {
      var _newTarget = this.constructor;
      if (errorMessage === void 0) {
        errorMessage = "A timeout occurred.";
      }
      var _this = this;
      var trueProto = _newTarget.prototype;
      _this = _super.call(this, errorMessage) || this;
      _this.__proto__ = trueProto;
      return _this;
    }
    return TimeoutError2;
  }(Error)
);

// node_modules/@aspnet/signalr/dist/esm/ILogger.js
var LogLevel;
(function(LogLevel2) {
  LogLevel2[LogLevel2["Trace"] = 0] = "Trace";
  LogLevel2[LogLevel2["Debug"] = 1] = "Debug";
  LogLevel2[LogLevel2["Information"] = 2] = "Information";
  LogLevel2[LogLevel2["Warning"] = 3] = "Warning";
  LogLevel2[LogLevel2["Error"] = 4] = "Error";
  LogLevel2[LogLevel2["Critical"] = 5] = "Critical";
  LogLevel2[LogLevel2["None"] = 6] = "None";
})(LogLevel || (LogLevel = {}));

// node_modules/@aspnet/signalr/dist/esm/HttpClient.js
var __extends2 = function() {
  var extendStatics = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function(d, b) {
    d.__proto__ = b;
  } || function(d, b) {
    for (var p in b)
      if (b.hasOwnProperty(p))
        d[p] = b[p];
  };
  return function(d, b) {
    extendStatics(d, b);
    function __() {
      this.constructor = d;
    }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
  };
}();
var __assign = Object.assign || function(t) {
  for (var s, i = 1, n = arguments.length; i < n; i++) {
    s = arguments[i];
    for (var p in s)
      if (Object.prototype.hasOwnProperty.call(s, p))
        t[p] = s[p];
  }
  return t;
};
var HttpResponse = (
  /** @class */
  /* @__PURE__ */ function() {
    function HttpResponse2(statusCode, statusText, content) {
      this.statusCode = statusCode;
      this.statusText = statusText;
      this.content = content;
    }
    return HttpResponse2;
  }()
);
var HttpClient = (
  /** @class */
  function() {
    function HttpClient2() {
    }
    HttpClient2.prototype.get = function(url, options) {
      return this.send(__assign({}, options, { method: "GET", url }));
    };
    HttpClient2.prototype.post = function(url, options) {
      return this.send(__assign({}, options, { method: "POST", url }));
    };
    HttpClient2.prototype.delete = function(url, options) {
      return this.send(__assign({}, options, { method: "DELETE", url }));
    };
    return HttpClient2;
  }()
);
var DefaultHttpClient = (
  /** @class */
  function(_super) {
    __extends2(DefaultHttpClient2, _super);
    function DefaultHttpClient2(logger) {
      var _this = _super.call(this) || this;
      _this.logger = logger;
      return _this;
    }
    DefaultHttpClient2.prototype.send = function(request) {
      var _this = this;
      return new Promise(function(resolve, reject) {
        var xhr = new XMLHttpRequest();
        xhr.open(request.method, request.url, true);
        xhr.withCredentials = true;
        xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        xhr.setRequestHeader("Content-Type", "text/plain;charset=UTF-8");
        if (request.headers) {
          Object.keys(request.headers).forEach(function(header) {
            return xhr.setRequestHeader(header, request.headers[header]);
          });
        }
        if (request.responseType) {
          xhr.responseType = request.responseType;
        }
        if (request.abortSignal) {
          request.abortSignal.onabort = function() {
            xhr.abort();
          };
        }
        if (request.timeout) {
          xhr.timeout = request.timeout;
        }
        xhr.onload = function() {
          if (request.abortSignal) {
            request.abortSignal.onabort = null;
          }
          if (xhr.status >= 200 && xhr.status < 300) {
            resolve(new HttpResponse(xhr.status, xhr.statusText, xhr.response || xhr.responseText));
          } else {
            reject(new HttpError(xhr.statusText, xhr.status));
          }
        };
        xhr.onerror = function() {
          _this.logger.log(LogLevel.Warning, "Error from HTTP request. " + xhr.status + ": " + xhr.statusText);
          reject(new HttpError(xhr.statusText, xhr.status));
        };
        xhr.ontimeout = function() {
          _this.logger.log(LogLevel.Warning, "Timeout from HTTP request.");
          reject(new TimeoutError());
        };
        xhr.send(request.content || "");
      });
    };
    return DefaultHttpClient2;
  }(HttpClient)
);

// node_modules/@aspnet/signalr/dist/esm/TextMessageFormat.js
var TextMessageFormat = (
  /** @class */
  function() {
    function TextMessageFormat2() {
    }
    TextMessageFormat2.write = function(output) {
      return "" + output + TextMessageFormat2.RecordSeparator;
    };
    TextMessageFormat2.parse = function(input) {
      if (input[input.length - 1] !== TextMessageFormat2.RecordSeparator) {
        throw new Error("Message is incomplete.");
      }
      var messages = input.split(TextMessageFormat2.RecordSeparator);
      messages.pop();
      return messages;
    };
    TextMessageFormat2.RecordSeparatorCode = 30;
    TextMessageFormat2.RecordSeparator = String.fromCharCode(TextMessageFormat2.RecordSeparatorCode);
    return TextMessageFormat2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/Loggers.js
var NullLogger = (
  /** @class */
  function() {
    function NullLogger2() {
    }
    NullLogger2.prototype.log = function(_logLevel, _message) {
    };
    NullLogger2.instance = new NullLogger2();
    return NullLogger2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/Utils.js
var __awaiter = function(thisArg, _arguments, P, generator) {
  return new (P || (P = Promise))(function(resolve, reject) {
    function fulfilled(value) {
      try {
        step(generator.next(value));
      } catch (e) {
        reject(e);
      }
    }
    function rejected(value) {
      try {
        step(generator["throw"](value));
      } catch (e) {
        reject(e);
      }
    }
    function step(result) {
      result.done ? resolve(result.value) : new P(function(resolve2) {
        resolve2(result.value);
      }).then(fulfilled, rejected);
    }
    step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
};
var __generator = function(thisArg, body) {
  var _ = { label: 0, sent: function() {
    if (t[0] & 1)
      throw t[1];
    return t[1];
  }, trys: [], ops: [] }, f, y, t, g;
  return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() {
    return this;
  }), g;
  function verb(n) {
    return function(v) {
      return step([n, v]);
    };
  }
  function step(op) {
    if (f)
      throw new TypeError("Generator is already executing.");
    while (_)
      try {
        if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done)
          return t;
        if (y = 0, t)
          op = [op[0] & 2, t.value];
        switch (op[0]) {
          case 0:
          case 1:
            t = op;
            break;
          case 4:
            _.label++;
            return { value: op[1], done: false };
          case 5:
            _.label++;
            y = op[1];
            op = [0];
            continue;
          case 7:
            op = _.ops.pop();
            _.trys.pop();
            continue;
          default:
            if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) {
              _ = 0;
              continue;
            }
            if (op[0] === 3 && (!t || op[1] > t[0] && op[1] < t[3])) {
              _.label = op[1];
              break;
            }
            if (op[0] === 6 && _.label < t[1]) {
              _.label = t[1];
              t = op;
              break;
            }
            if (t && _.label < t[2]) {
              _.label = t[2];
              _.ops.push(op);
              break;
            }
            if (t[2])
              _.ops.pop();
            _.trys.pop();
            continue;
        }
        op = body.call(thisArg, _);
      } catch (e) {
        op = [6, e];
        y = 0;
      } finally {
        f = t = 0;
      }
    if (op[0] & 5)
      throw op[1];
    return { value: op[0] ? op[1] : void 0, done: true };
  }
};
var Arg = (
  /** @class */
  function() {
    function Arg2() {
    }
    Arg2.isRequired = function(val, name) {
      if (val === null || val === void 0) {
        throw new Error("The '" + name + "' argument is required.");
      }
    };
    Arg2.isIn = function(val, values, name) {
      if (!(val in values)) {
        throw new Error("Unknown " + name + " value: " + val + ".");
      }
    };
    return Arg2;
  }()
);
function getDataDetail(data, includeContent) {
  var length = null;
  if (isArrayBuffer(data)) {
    length = "Binary data of length " + data.byteLength;
    if (includeContent) {
      length += ". Content: '" + formatArrayBuffer(data) + "'";
    }
  } else if (typeof data === "string") {
    length = "String data of length " + data.length;
    if (includeContent) {
      length += ". Content: '" + data + "'.";
    }
  }
  return length;
}
function formatArrayBuffer(data) {
  var view = new Uint8Array(data);
  var str = "";
  view.forEach(function(num) {
    var pad = num < 16 ? "0" : "";
    str += "0x" + pad + num.toString(16) + " ";
  });
  return str.substr(0, str.length - 1);
}
function sendMessage(logger, transportName, httpClient, url, accessTokenFactory, content, logMessageContent) {
  return __awaiter(this, void 0, void 0, function() {
    var _a, headers, token, response;
    return __generator(this, function(_b) {
      switch (_b.label) {
        case 0:
          return [4, accessTokenFactory()];
        case 1:
          token = _b.sent();
          if (token) {
            headers = (_a = {}, _a["Authorization"] = "Bearer " + token, _a);
          }
          logger.log(LogLevel.Trace, "(" + transportName + " transport) sending data. " + getDataDetail(content, logMessageContent) + ".");
          return [4, httpClient.post(url, {
            content,
            headers
          })];
        case 2:
          response = _b.sent();
          logger.log(LogLevel.Trace, "(" + transportName + " transport) request complete. Response status: " + response.statusCode + ".");
          return [
            2
            /*return*/
          ];
      }
    });
  });
}
function createLogger(logger) {
  if (logger === void 0) {
    return new ConsoleLogger(LogLevel.Information);
  }
  if (logger === null) {
    return NullLogger.instance;
  }
  if (logger.log) {
    return logger;
  }
  return new ConsoleLogger(logger);
}
var Subject = (
  /** @class */
  function() {
    function Subject2(cancelCallback) {
      this.observers = [];
      this.cancelCallback = cancelCallback;
    }
    Subject2.prototype.next = function(item) {
      for (var _i = 0, _a = this.observers; _i < _a.length; _i++) {
        var observer = _a[_i];
        observer.next(item);
      }
    };
    Subject2.prototype.error = function(err) {
      for (var _i = 0, _a = this.observers; _i < _a.length; _i++) {
        var observer = _a[_i];
        if (observer.error) {
          observer.error(err);
        }
      }
    };
    Subject2.prototype.complete = function() {
      for (var _i = 0, _a = this.observers; _i < _a.length; _i++) {
        var observer = _a[_i];
        if (observer.complete) {
          observer.complete();
        }
      }
    };
    Subject2.prototype.subscribe = function(observer) {
      this.observers.push(observer);
      return new SubjectSubscription(this, observer);
    };
    return Subject2;
  }()
);
var SubjectSubscription = (
  /** @class */
  function() {
    function SubjectSubscription2(subject, observer) {
      this.subject = subject;
      this.observer = observer;
    }
    SubjectSubscription2.prototype.dispose = function() {
      var index = this.subject.observers.indexOf(this.observer);
      if (index > -1) {
        this.subject.observers.splice(index, 1);
      }
      if (this.subject.observers.length === 0) {
        this.subject.cancelCallback().catch(function(_) {
        });
      }
    };
    return SubjectSubscription2;
  }()
);
var ConsoleLogger = (
  /** @class */
  function() {
    function ConsoleLogger2(minimumLogLevel) {
      this.minimumLogLevel = minimumLogLevel;
    }
    ConsoleLogger2.prototype.log = function(logLevel, message) {
      if (logLevel >= this.minimumLogLevel) {
        switch (logLevel) {
          case LogLevel.Critical:
          case LogLevel.Error:
            console.error(LogLevel[logLevel] + ": " + message);
            break;
          case LogLevel.Warning:
            console.warn(LogLevel[logLevel] + ": " + message);
            break;
          case LogLevel.Information:
            console.info(LogLevel[logLevel] + ": " + message);
            break;
          default:
            console.log(LogLevel[logLevel] + ": " + message);
            break;
        }
      }
    };
    return ConsoleLogger2;
  }()
);
function isArrayBuffer(val) {
  return val && typeof ArrayBuffer !== "undefined" && (val instanceof ArrayBuffer || // Sometimes we get an ArrayBuffer that doesn't satisfy instanceof
  val.constructor && val.constructor.name === "ArrayBuffer");
}

// node_modules/@aspnet/signalr/dist/esm/HandshakeProtocol.js
var HandshakeProtocol = (
  /** @class */
  function() {
    function HandshakeProtocol2() {
    }
    HandshakeProtocol2.prototype.writeHandshakeRequest = function(handshakeRequest) {
      return TextMessageFormat.write(JSON.stringify(handshakeRequest));
    };
    HandshakeProtocol2.prototype.parseHandshakeResponse = function(data) {
      var responseMessage;
      var messageData;
      var remainingData;
      if (isArrayBuffer(data)) {
        var binaryData = new Uint8Array(data);
        var separatorIndex = binaryData.indexOf(TextMessageFormat.RecordSeparatorCode);
        if (separatorIndex === -1) {
          throw new Error("Message is incomplete.");
        }
        var responseLength = separatorIndex + 1;
        messageData = String.fromCharCode.apply(null, binaryData.slice(0, responseLength));
        remainingData = binaryData.byteLength > responseLength ? binaryData.slice(responseLength).buffer : null;
      } else {
        var textData = data;
        var separatorIndex = textData.indexOf(TextMessageFormat.RecordSeparator);
        if (separatorIndex === -1) {
          throw new Error("Message is incomplete.");
        }
        var responseLength = separatorIndex + 1;
        messageData = textData.substring(0, responseLength);
        remainingData = textData.length > responseLength ? textData.substring(responseLength) : null;
      }
      var messages = TextMessageFormat.parse(messageData);
      responseMessage = JSON.parse(messages[0]);
      return [remainingData, responseMessage];
    };
    return HandshakeProtocol2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/IHubProtocol.js
var MessageType;
(function(MessageType2) {
  MessageType2[MessageType2["Invocation"] = 1] = "Invocation";
  MessageType2[MessageType2["StreamItem"] = 2] = "StreamItem";
  MessageType2[MessageType2["Completion"] = 3] = "Completion";
  MessageType2[MessageType2["StreamInvocation"] = 4] = "StreamInvocation";
  MessageType2[MessageType2["CancelInvocation"] = 5] = "CancelInvocation";
  MessageType2[MessageType2["Ping"] = 6] = "Ping";
  MessageType2[MessageType2["Close"] = 7] = "Close";
})(MessageType || (MessageType = {}));

// node_modules/@aspnet/signalr/dist/esm/HubConnection.js
var __awaiter2 = function(thisArg, _arguments, P, generator) {
  return new (P || (P = Promise))(function(resolve, reject) {
    function fulfilled(value) {
      try {
        step(generator.next(value));
      } catch (e) {
        reject(e);
      }
    }
    function rejected(value) {
      try {
        step(generator["throw"](value));
      } catch (e) {
        reject(e);
      }
    }
    function step(result) {
      result.done ? resolve(result.value) : new P(function(resolve2) {
        resolve2(result.value);
      }).then(fulfilled, rejected);
    }
    step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
};
var __generator2 = function(thisArg, body) {
  var _ = { label: 0, sent: function() {
    if (t[0] & 1)
      throw t[1];
    return t[1];
  }, trys: [], ops: [] }, f, y, t, g;
  return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() {
    return this;
  }), g;
  function verb(n) {
    return function(v) {
      return step([n, v]);
    };
  }
  function step(op) {
    if (f)
      throw new TypeError("Generator is already executing.");
    while (_)
      try {
        if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done)
          return t;
        if (y = 0, t)
          op = [op[0] & 2, t.value];
        switch (op[0]) {
          case 0:
          case 1:
            t = op;
            break;
          case 4:
            _.label++;
            return { value: op[1], done: false };
          case 5:
            _.label++;
            y = op[1];
            op = [0];
            continue;
          case 7:
            op = _.ops.pop();
            _.trys.pop();
            continue;
          default:
            if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) {
              _ = 0;
              continue;
            }
            if (op[0] === 3 && (!t || op[1] > t[0] && op[1] < t[3])) {
              _.label = op[1];
              break;
            }
            if (op[0] === 6 && _.label < t[1]) {
              _.label = t[1];
              t = op;
              break;
            }
            if (t && _.label < t[2]) {
              _.label = t[2];
              _.ops.push(op);
              break;
            }
            if (t[2])
              _.ops.pop();
            _.trys.pop();
            continue;
        }
        op = body.call(thisArg, _);
      } catch (e) {
        op = [6, e];
        y = 0;
      } finally {
        f = t = 0;
      }
    if (op[0] & 5)
      throw op[1];
    return { value: op[0] ? op[1] : void 0, done: true };
  }
};
var DEFAULT_TIMEOUT_IN_MS = 30 * 1e3;
var HubConnection = (
  /** @class */
  function() {
    function HubConnection2(connection, logger, protocol) {
      var _this = this;
      Arg.isRequired(connection, "connection");
      Arg.isRequired(logger, "logger");
      Arg.isRequired(protocol, "protocol");
      this.serverTimeoutInMilliseconds = DEFAULT_TIMEOUT_IN_MS;
      this.logger = logger;
      this.protocol = protocol;
      this.connection = connection;
      this.handshakeProtocol = new HandshakeProtocol();
      this.connection.onreceive = function(data) {
        return _this.processIncomingData(data);
      };
      this.connection.onclose = function(error) {
        return _this.connectionClosed(error);
      };
      this.callbacks = {};
      this.methods = {};
      this.closedCallbacks = [];
      this.id = 0;
    }
    HubConnection2.create = function(connection, logger, protocol) {
      return new HubConnection2(connection, logger, protocol);
    };
    HubConnection2.prototype.start = function() {
      return __awaiter2(this, void 0, void 0, function() {
        var handshakeRequest;
        return __generator2(this, function(_a) {
          switch (_a.label) {
            case 0:
              handshakeRequest = {
                protocol: this.protocol.name,
                version: this.protocol.version
              };
              this.logger.log(LogLevel.Debug, "Starting HubConnection.");
              this.receivedHandshakeResponse = false;
              return [4, this.connection.start(this.protocol.transferFormat)];
            case 1:
              _a.sent();
              this.logger.log(LogLevel.Debug, "Sending handshake request.");
              return [4, this.connection.send(this.handshakeProtocol.writeHandshakeRequest(handshakeRequest))];
            case 2:
              _a.sent();
              this.logger.log(LogLevel.Information, "Using HubProtocol '" + this.protocol.name + "'.");
              this.cleanupTimeout();
              this.configureTimeout();
              return [
                2
                /*return*/
              ];
          }
        });
      });
    };
    HubConnection2.prototype.stop = function() {
      this.logger.log(LogLevel.Debug, "Stopping HubConnection.");
      this.cleanupTimeout();
      return this.connection.stop();
    };
    HubConnection2.prototype.stream = function(methodName) {
      var _this = this;
      var args = [];
      for (var _i = 1; _i < arguments.length; _i++) {
        args[_i - 1] = arguments[_i];
      }
      var invocationDescriptor = this.createStreamInvocation(methodName, args);
      var subject = new Subject(function() {
        var cancelInvocation = _this.createCancelInvocation(invocationDescriptor.invocationId);
        var cancelMessage = _this.protocol.writeMessage(cancelInvocation);
        delete _this.callbacks[invocationDescriptor.invocationId];
        return _this.connection.send(cancelMessage);
      });
      this.callbacks[invocationDescriptor.invocationId] = function(invocationEvent, error) {
        if (error) {
          subject.error(error);
          return;
        }
        if (invocationEvent.type === MessageType.Completion) {
          if (invocationEvent.error) {
            subject.error(new Error(invocationEvent.error));
          } else {
            subject.complete();
          }
        } else {
          subject.next(invocationEvent.item);
        }
      };
      var message = this.protocol.writeMessage(invocationDescriptor);
      this.connection.send(message).catch(function(e) {
        subject.error(e);
        delete _this.callbacks[invocationDescriptor.invocationId];
      });
      return subject;
    };
    HubConnection2.prototype.send = function(methodName) {
      var args = [];
      for (var _i = 1; _i < arguments.length; _i++) {
        args[_i - 1] = arguments[_i];
      }
      var invocationDescriptor = this.createInvocation(methodName, args, true);
      var message = this.protocol.writeMessage(invocationDescriptor);
      return this.connection.send(message);
    };
    HubConnection2.prototype.invoke = function(methodName) {
      var _this = this;
      var args = [];
      for (var _i = 1; _i < arguments.length; _i++) {
        args[_i - 1] = arguments[_i];
      }
      var invocationDescriptor = this.createInvocation(methodName, args, false);
      var p = new Promise(function(resolve, reject) {
        _this.callbacks[invocationDescriptor.invocationId] = function(invocationEvent, error) {
          if (error) {
            reject(error);
            return;
          }
          if (invocationEvent.type === MessageType.Completion) {
            var completionMessage = invocationEvent;
            if (completionMessage.error) {
              reject(new Error(completionMessage.error));
            } else {
              resolve(completionMessage.result);
            }
          } else {
            reject(new Error("Unexpected message type: " + invocationEvent.type));
          }
        };
        var message = _this.protocol.writeMessage(invocationDescriptor);
        _this.connection.send(message).catch(function(e) {
          reject(e);
          delete _this.callbacks[invocationDescriptor.invocationId];
        });
      });
      return p;
    };
    HubConnection2.prototype.on = function(methodName, newMethod) {
      if (!methodName || !newMethod) {
        return;
      }
      methodName = methodName.toLowerCase();
      if (!this.methods[methodName]) {
        this.methods[methodName] = [];
      }
      if (this.methods[methodName].indexOf(newMethod) !== -1) {
        return;
      }
      this.methods[methodName].push(newMethod);
    };
    HubConnection2.prototype.off = function(methodName, method) {
      if (!methodName) {
        return;
      }
      methodName = methodName.toLowerCase();
      var handlers = this.methods[methodName];
      if (!handlers) {
        return;
      }
      if (method) {
        var removeIdx = handlers.indexOf(method);
        if (removeIdx !== -1) {
          handlers.splice(removeIdx, 1);
          if (handlers.length === 0) {
            delete this.methods[methodName];
          }
        }
      } else {
        delete this.methods[methodName];
      }
    };
    HubConnection2.prototype.onclose = function(callback) {
      if (callback) {
        this.closedCallbacks.push(callback);
      }
    };
    HubConnection2.prototype.processIncomingData = function(data) {
      this.cleanupTimeout();
      if (!this.receivedHandshakeResponse) {
        data = this.processHandshakeResponse(data);
        this.receivedHandshakeResponse = true;
      }
      if (data) {
        var messages = this.protocol.parseMessages(data, this.logger);
        for (var _i = 0, messages_1 = messages; _i < messages_1.length; _i++) {
          var message = messages_1[_i];
          switch (message.type) {
            case MessageType.Invocation:
              this.invokeClientMethod(message);
              break;
            case MessageType.StreamItem:
            case MessageType.Completion:
              var callback = this.callbacks[message.invocationId];
              if (callback != null) {
                if (message.type === MessageType.Completion) {
                  delete this.callbacks[message.invocationId];
                }
                callback(message);
              }
              break;
            case MessageType.Ping:
              break;
            case MessageType.Close:
              this.logger.log(LogLevel.Information, "Close message received from server.");
              this.connection.stop(message.error ? new Error("Server returned an error on close: " + message.error) : null);
              break;
            default:
              this.logger.log(LogLevel.Warning, "Invalid message type: " + message.type);
              break;
          }
        }
      }
      this.configureTimeout();
    };
    HubConnection2.prototype.processHandshakeResponse = function(data) {
      var _a;
      var responseMessage;
      var remainingData;
      try {
        _a = this.handshakeProtocol.parseHandshakeResponse(data), remainingData = _a[0], responseMessage = _a[1];
      } catch (e) {
        var message = "Error parsing handshake response: " + e;
        this.logger.log(LogLevel.Error, message);
        var error = new Error(message);
        this.connection.stop(error);
        throw error;
      }
      if (responseMessage.error) {
        var message = "Server returned handshake error: " + responseMessage.error;
        this.logger.log(LogLevel.Error, message);
        this.connection.stop(new Error(message));
      } else {
        this.logger.log(LogLevel.Debug, "Server handshake complete.");
      }
      return remainingData;
    };
    HubConnection2.prototype.configureTimeout = function() {
      var _this = this;
      if (!this.connection.features || !this.connection.features.inherentKeepAlive) {
        this.timeoutHandle = setTimeout(function() {
          return _this.serverTimeout();
        }, this.serverTimeoutInMilliseconds);
      }
    };
    HubConnection2.prototype.serverTimeout = function() {
      this.connection.stop(new Error("Server timeout elapsed without receiving a message from the server."));
    };
    HubConnection2.prototype.invokeClientMethod = function(invocationMessage) {
      var _this = this;
      var methods = this.methods[invocationMessage.target.toLowerCase()];
      if (methods) {
        methods.forEach(function(m) {
          return m.apply(_this, invocationMessage.arguments);
        });
        if (invocationMessage.invocationId) {
          var message = "Server requested a response, which is not supported in this version of the client.";
          this.logger.log(LogLevel.Error, message);
          this.connection.stop(new Error(message));
        }
      } else {
        this.logger.log(LogLevel.Warning, "No client method with the name '" + invocationMessage.target + "' found.");
      }
    };
    HubConnection2.prototype.connectionClosed = function(error) {
      var _this = this;
      var callbacks = this.callbacks;
      this.callbacks = {};
      Object.keys(callbacks).forEach(function(key) {
        var callback = callbacks[key];
        callback(void 0, error ? error : new Error("Invocation canceled due to connection being closed."));
      });
      this.cleanupTimeout();
      this.closedCallbacks.forEach(function(c) {
        return c.apply(_this, [error]);
      });
    };
    HubConnection2.prototype.cleanupTimeout = function() {
      if (this.timeoutHandle) {
        clearTimeout(this.timeoutHandle);
      }
    };
    HubConnection2.prototype.createInvocation = function(methodName, args, nonblocking) {
      if (nonblocking) {
        return {
          arguments: args,
          target: methodName,
          type: MessageType.Invocation
        };
      } else {
        var id = this.id;
        this.id++;
        return {
          arguments: args,
          invocationId: id.toString(),
          target: methodName,
          type: MessageType.Invocation
        };
      }
    };
    HubConnection2.prototype.createStreamInvocation = function(methodName, args) {
      var id = this.id;
      this.id++;
      return {
        arguments: args,
        invocationId: id.toString(),
        target: methodName,
        type: MessageType.StreamInvocation
      };
    };
    HubConnection2.prototype.createCancelInvocation = function(id) {
      return {
        invocationId: id,
        type: MessageType.CancelInvocation
      };
    };
    return HubConnection2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/ITransport.js
var HttpTransportType;
(function(HttpTransportType2) {
  HttpTransportType2[HttpTransportType2["None"] = 0] = "None";
  HttpTransportType2[HttpTransportType2["WebSockets"] = 1] = "WebSockets";
  HttpTransportType2[HttpTransportType2["ServerSentEvents"] = 2] = "ServerSentEvents";
  HttpTransportType2[HttpTransportType2["LongPolling"] = 4] = "LongPolling";
})(HttpTransportType || (HttpTransportType = {}));
var TransferFormat;
(function(TransferFormat2) {
  TransferFormat2[TransferFormat2["Text"] = 1] = "Text";
  TransferFormat2[TransferFormat2["Binary"] = 2] = "Binary";
})(TransferFormat || (TransferFormat = {}));

// node_modules/@aspnet/signalr/dist/esm/AbortController.js
var AbortController = (
  /** @class */
  function() {
    function AbortController2() {
      this.isAborted = false;
    }
    AbortController2.prototype.abort = function() {
      if (!this.isAborted) {
        this.isAborted = true;
        if (this.onabort) {
          this.onabort();
        }
      }
    };
    Object.defineProperty(AbortController2.prototype, "signal", {
      get: function() {
        return this;
      },
      enumerable: true,
      configurable: true
    });
    Object.defineProperty(AbortController2.prototype, "aborted", {
      get: function() {
        return this.isAborted;
      },
      enumerable: true,
      configurable: true
    });
    return AbortController2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/LongPollingTransport.js
var __awaiter3 = function(thisArg, _arguments, P, generator) {
  return new (P || (P = Promise))(function(resolve, reject) {
    function fulfilled(value) {
      try {
        step(generator.next(value));
      } catch (e) {
        reject(e);
      }
    }
    function rejected(value) {
      try {
        step(generator["throw"](value));
      } catch (e) {
        reject(e);
      }
    }
    function step(result) {
      result.done ? resolve(result.value) : new P(function(resolve2) {
        resolve2(result.value);
      }).then(fulfilled, rejected);
    }
    step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
};
var __generator3 = function(thisArg, body) {
  var _ = { label: 0, sent: function() {
    if (t[0] & 1)
      throw t[1];
    return t[1];
  }, trys: [], ops: [] }, f, y, t, g;
  return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() {
    return this;
  }), g;
  function verb(n) {
    return function(v) {
      return step([n, v]);
    };
  }
  function step(op) {
    if (f)
      throw new TypeError("Generator is already executing.");
    while (_)
      try {
        if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done)
          return t;
        if (y = 0, t)
          op = [op[0] & 2, t.value];
        switch (op[0]) {
          case 0:
          case 1:
            t = op;
            break;
          case 4:
            _.label++;
            return { value: op[1], done: false };
          case 5:
            _.label++;
            y = op[1];
            op = [0];
            continue;
          case 7:
            op = _.ops.pop();
            _.trys.pop();
            continue;
          default:
            if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) {
              _ = 0;
              continue;
            }
            if (op[0] === 3 && (!t || op[1] > t[0] && op[1] < t[3])) {
              _.label = op[1];
              break;
            }
            if (op[0] === 6 && _.label < t[1]) {
              _.label = t[1];
              t = op;
              break;
            }
            if (t && _.label < t[2]) {
              _.label = t[2];
              _.ops.push(op);
              break;
            }
            if (t[2])
              _.ops.pop();
            _.trys.pop();
            continue;
        }
        op = body.call(thisArg, _);
      } catch (e) {
        op = [6, e];
        y = 0;
      } finally {
        f = t = 0;
      }
    if (op[0] & 5)
      throw op[1];
    return { value: op[0] ? op[1] : void 0, done: true };
  }
};
var SHUTDOWN_TIMEOUT = 5 * 1e3;
var LongPollingTransport = (
  /** @class */
  function() {
    function LongPollingTransport2(httpClient, accessTokenFactory, logger, logMessageContent, shutdownTimeout) {
      this.httpClient = httpClient;
      this.accessTokenFactory = accessTokenFactory || function() {
        return null;
      };
      this.logger = logger;
      this.pollAbort = new AbortController();
      this.logMessageContent = logMessageContent;
      this.shutdownTimeout = shutdownTimeout || SHUTDOWN_TIMEOUT;
    }
    Object.defineProperty(LongPollingTransport2.prototype, "pollAborted", {
      // This is an internal type, not exported from 'index' so this is really just internal.
      get: function() {
        return this.pollAbort.aborted;
      },
      enumerable: true,
      configurable: true
    });
    LongPollingTransport2.prototype.connect = function(url, transferFormat) {
      return __awaiter3(this, void 0, void 0, function() {
        var pollOptions, token, closeError, pollUrl, response;
        return __generator3(this, function(_a) {
          switch (_a.label) {
            case 0:
              Arg.isRequired(url, "url");
              Arg.isRequired(transferFormat, "transferFormat");
              Arg.isIn(transferFormat, TransferFormat, "transferFormat");
              this.url = url;
              this.logger.log(LogLevel.Trace, "(LongPolling transport) Connecting");
              if (transferFormat === TransferFormat.Binary && typeof new XMLHttpRequest().responseType !== "string") {
                throw new Error("Binary protocols over XmlHttpRequest not implementing advanced features are not supported.");
              }
              pollOptions = {
                abortSignal: this.pollAbort.signal,
                headers: {},
                timeout: 9e4
              };
              if (transferFormat === TransferFormat.Binary) {
                pollOptions.responseType = "arraybuffer";
              }
              return [4, this.accessTokenFactory()];
            case 1:
              token = _a.sent();
              this.updateHeaderToken(pollOptions, token);
              pollUrl = url + "&_=" + Date.now();
              this.logger.log(LogLevel.Trace, "(LongPolling transport) polling: " + pollUrl);
              return [4, this.httpClient.get(pollUrl, pollOptions)];
            case 2:
              response = _a.sent();
              if (response.statusCode !== 200) {
                this.logger.log(LogLevel.Error, "(LongPolling transport) Unexpected response code: " + response.statusCode);
                closeError = new HttpError(response.statusText, response.statusCode);
                this.running = false;
              } else {
                this.running = true;
              }
              this.poll(this.url, pollOptions, closeError);
              return [2, Promise.resolve()];
          }
        });
      });
    };
    LongPollingTransport2.prototype.updateHeaderToken = function(request, token) {
      if (token) {
        request.headers["Authorization"] = "Bearer " + token;
        return;
      }
      if (request.headers["Authorization"]) {
        delete request.headers["Authorization"];
      }
    };
    LongPollingTransport2.prototype.poll = function(url, pollOptions, closeError) {
      return __awaiter3(this, void 0, void 0, function() {
        var token, pollUrl, response, e_1;
        return __generator3(this, function(_a) {
          switch (_a.label) {
            case 0:
              _a.trys.push([0, , 8, 9]);
              _a.label = 1;
            case 1:
              if (!this.running)
                return [3, 7];
              return [4, this.accessTokenFactory()];
            case 2:
              token = _a.sent();
              this.updateHeaderToken(pollOptions, token);
              _a.label = 3;
            case 3:
              _a.trys.push([3, 5, , 6]);
              pollUrl = url + "&_=" + Date.now();
              this.logger.log(LogLevel.Trace, "(LongPolling transport) polling: " + pollUrl);
              return [4, this.httpClient.get(pollUrl, pollOptions)];
            case 4:
              response = _a.sent();
              if (response.statusCode === 204) {
                this.logger.log(LogLevel.Information, "(LongPolling transport) Poll terminated by server");
                this.running = false;
              } else if (response.statusCode !== 200) {
                this.logger.log(LogLevel.Error, "(LongPolling transport) Unexpected response code: " + response.statusCode);
                closeError = new HttpError(response.statusText, response.statusCode);
                this.running = false;
              } else {
                if (response.content) {
                  this.logger.log(LogLevel.Trace, "(LongPolling transport) data received. " + getDataDetail(response.content, this.logMessageContent));
                  if (this.onreceive) {
                    this.onreceive(response.content);
                  }
                } else {
                  this.logger.log(LogLevel.Trace, "(LongPolling transport) Poll timed out, reissuing.");
                }
              }
              return [3, 6];
            case 5:
              e_1 = _a.sent();
              if (!this.running) {
                this.logger.log(LogLevel.Trace, "(LongPolling transport) Poll errored after shutdown: " + e_1.message);
              } else {
                if (e_1 instanceof TimeoutError) {
                  this.logger.log(LogLevel.Trace, "(LongPolling transport) Poll timed out, reissuing.");
                } else {
                  closeError = e_1;
                  this.running = false;
                }
              }
              return [3, 6];
            case 6:
              return [3, 1];
            case 7:
              return [3, 9];
            case 8:
              this.stopped = true;
              if (this.shutdownTimer) {
                clearTimeout(this.shutdownTimer);
              }
              if (this.onclose) {
                this.logger.log(LogLevel.Trace, "(LongPolling transport) Firing onclose event. Error: " + (closeError || "<undefined>"));
                this.onclose(closeError);
              }
              this.logger.log(LogLevel.Trace, "(LongPolling transport) Transport finished.");
              return [
                7
                /*endfinally*/
              ];
            case 9:
              return [
                2
                /*return*/
              ];
          }
        });
      });
    };
    LongPollingTransport2.prototype.send = function(data) {
      return __awaiter3(this, void 0, void 0, function() {
        return __generator3(this, function(_a) {
          if (!this.running) {
            return [2, Promise.reject(new Error("Cannot send until the transport is connected"))];
          }
          return [2, sendMessage(this.logger, "LongPolling", this.httpClient, this.url, this.accessTokenFactory, data, this.logMessageContent)];
        });
      });
    };
    LongPollingTransport2.prototype.stop = function() {
      return __awaiter3(this, void 0, void 0, function() {
        var deleteOptions, token;
        var _this = this;
        return __generator3(this, function(_a) {
          switch (_a.label) {
            case 0:
              _a.trys.push([0, , 3, 4]);
              this.running = false;
              this.logger.log(LogLevel.Trace, "(LongPolling transport) sending DELETE request to " + this.url + ".");
              deleteOptions = {
                headers: {}
              };
              return [4, this.accessTokenFactory()];
            case 1:
              token = _a.sent();
              this.updateHeaderToken(deleteOptions, token);
              return [4, this.httpClient.delete(this.url, deleteOptions)];
            case 2:
              _a.sent();
              this.logger.log(LogLevel.Trace, "(LongPolling transport) DELETE request accepted.");
              return [3, 4];
            case 3:
              if (!this.stopped) {
                this.shutdownTimer = setTimeout(function() {
                  _this.logger.log(LogLevel.Warning, "(LongPolling transport) server did not terminate after DELETE request, canceling poll.");
                  _this.pollAbort.abort();
                }, this.shutdownTimeout);
              }
              return [
                7
                /*endfinally*/
              ];
            case 4:
              return [
                2
                /*return*/
              ];
          }
        });
      });
    };
    return LongPollingTransport2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/ServerSentEventsTransport.js
var __awaiter4 = function(thisArg, _arguments, P, generator) {
  return new (P || (P = Promise))(function(resolve, reject) {
    function fulfilled(value) {
      try {
        step(generator.next(value));
      } catch (e) {
        reject(e);
      }
    }
    function rejected(value) {
      try {
        step(generator["throw"](value));
      } catch (e) {
        reject(e);
      }
    }
    function step(result) {
      result.done ? resolve(result.value) : new P(function(resolve2) {
        resolve2(result.value);
      }).then(fulfilled, rejected);
    }
    step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
};
var __generator4 = function(thisArg, body) {
  var _ = { label: 0, sent: function() {
    if (t[0] & 1)
      throw t[1];
    return t[1];
  }, trys: [], ops: [] }, f, y, t, g;
  return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() {
    return this;
  }), g;
  function verb(n) {
    return function(v) {
      return step([n, v]);
    };
  }
  function step(op) {
    if (f)
      throw new TypeError("Generator is already executing.");
    while (_)
      try {
        if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done)
          return t;
        if (y = 0, t)
          op = [op[0] & 2, t.value];
        switch (op[0]) {
          case 0:
          case 1:
            t = op;
            break;
          case 4:
            _.label++;
            return { value: op[1], done: false };
          case 5:
            _.label++;
            y = op[1];
            op = [0];
            continue;
          case 7:
            op = _.ops.pop();
            _.trys.pop();
            continue;
          default:
            if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) {
              _ = 0;
              continue;
            }
            if (op[0] === 3 && (!t || op[1] > t[0] && op[1] < t[3])) {
              _.label = op[1];
              break;
            }
            if (op[0] === 6 && _.label < t[1]) {
              _.label = t[1];
              t = op;
              break;
            }
            if (t && _.label < t[2]) {
              _.label = t[2];
              _.ops.push(op);
              break;
            }
            if (t[2])
              _.ops.pop();
            _.trys.pop();
            continue;
        }
        op = body.call(thisArg, _);
      } catch (e) {
        op = [6, e];
        y = 0;
      } finally {
        f = t = 0;
      }
    if (op[0] & 5)
      throw op[1];
    return { value: op[0] ? op[1] : void 0, done: true };
  }
};
var ServerSentEventsTransport = (
  /** @class */
  function() {
    function ServerSentEventsTransport2(httpClient, accessTokenFactory, logger, logMessageContent) {
      this.httpClient = httpClient;
      this.accessTokenFactory = accessTokenFactory || function() {
        return null;
      };
      this.logger = logger;
      this.logMessageContent = logMessageContent;
    }
    ServerSentEventsTransport2.prototype.connect = function(url, transferFormat) {
      return __awaiter4(this, void 0, void 0, function() {
        var token;
        var _this = this;
        return __generator4(this, function(_a) {
          switch (_a.label) {
            case 0:
              Arg.isRequired(url, "url");
              Arg.isRequired(transferFormat, "transferFormat");
              Arg.isIn(transferFormat, TransferFormat, "transferFormat");
              if (typeof EventSource === "undefined") {
                throw new Error("'EventSource' is not supported in your environment.");
              }
              this.logger.log(LogLevel.Trace, "(SSE transport) Connecting");
              return [4, this.accessTokenFactory()];
            case 1:
              token = _a.sent();
              if (token) {
                url += (url.indexOf("?") < 0 ? "?" : "&") + ("access_token=" + encodeURIComponent(token));
              }
              this.url = url;
              return [2, new Promise(function(resolve, reject) {
                var opened = false;
                if (transferFormat !== TransferFormat.Text) {
                  reject(new Error("The Server-Sent Events transport only supports the 'Text' transfer format"));
                }
                var eventSource = new EventSource(url, { withCredentials: true });
                try {
                  eventSource.onmessage = function(e) {
                    if (_this.onreceive) {
                      try {
                        _this.logger.log(LogLevel.Trace, "(SSE transport) data received. " + getDataDetail(e.data, _this.logMessageContent) + ".");
                        _this.onreceive(e.data);
                      } catch (error) {
                        if (_this.onclose) {
                          _this.onclose(error);
                        }
                        return;
                      }
                    }
                  };
                  eventSource.onerror = function(e) {
                    var error = new Error(e.message || "Error occurred");
                    if (opened) {
                      _this.close(error);
                    } else {
                      reject(error);
                    }
                  };
                  eventSource.onopen = function() {
                    _this.logger.log(LogLevel.Information, "SSE connected to " + _this.url);
                    _this.eventSource = eventSource;
                    opened = true;
                    resolve();
                  };
                } catch (e) {
                  return Promise.reject(e);
                }
              })];
          }
        });
      });
    };
    ServerSentEventsTransport2.prototype.send = function(data) {
      return __awaiter4(this, void 0, void 0, function() {
        return __generator4(this, function(_a) {
          if (!this.eventSource) {
            return [2, Promise.reject(new Error("Cannot send until the transport is connected"))];
          }
          return [2, sendMessage(this.logger, "SSE", this.httpClient, this.url, this.accessTokenFactory, data, this.logMessageContent)];
        });
      });
    };
    ServerSentEventsTransport2.prototype.stop = function() {
      this.close();
      return Promise.resolve();
    };
    ServerSentEventsTransport2.prototype.close = function(e) {
      if (this.eventSource) {
        this.eventSource.close();
        this.eventSource = null;
        if (this.onclose) {
          this.onclose(e);
        }
      }
    };
    return ServerSentEventsTransport2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/WebSocketTransport.js
var __awaiter5 = function(thisArg, _arguments, P, generator) {
  return new (P || (P = Promise))(function(resolve, reject) {
    function fulfilled(value) {
      try {
        step(generator.next(value));
      } catch (e) {
        reject(e);
      }
    }
    function rejected(value) {
      try {
        step(generator["throw"](value));
      } catch (e) {
        reject(e);
      }
    }
    function step(result) {
      result.done ? resolve(result.value) : new P(function(resolve2) {
        resolve2(result.value);
      }).then(fulfilled, rejected);
    }
    step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
};
var __generator5 = function(thisArg, body) {
  var _ = { label: 0, sent: function() {
    if (t[0] & 1)
      throw t[1];
    return t[1];
  }, trys: [], ops: [] }, f, y, t, g;
  return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() {
    return this;
  }), g;
  function verb(n) {
    return function(v) {
      return step([n, v]);
    };
  }
  function step(op) {
    if (f)
      throw new TypeError("Generator is already executing.");
    while (_)
      try {
        if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done)
          return t;
        if (y = 0, t)
          op = [op[0] & 2, t.value];
        switch (op[0]) {
          case 0:
          case 1:
            t = op;
            break;
          case 4:
            _.label++;
            return { value: op[1], done: false };
          case 5:
            _.label++;
            y = op[1];
            op = [0];
            continue;
          case 7:
            op = _.ops.pop();
            _.trys.pop();
            continue;
          default:
            if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) {
              _ = 0;
              continue;
            }
            if (op[0] === 3 && (!t || op[1] > t[0] && op[1] < t[3])) {
              _.label = op[1];
              break;
            }
            if (op[0] === 6 && _.label < t[1]) {
              _.label = t[1];
              t = op;
              break;
            }
            if (t && _.label < t[2]) {
              _.label = t[2];
              _.ops.push(op);
              break;
            }
            if (t[2])
              _.ops.pop();
            _.trys.pop();
            continue;
        }
        op = body.call(thisArg, _);
      } catch (e) {
        op = [6, e];
        y = 0;
      } finally {
        f = t = 0;
      }
    if (op[0] & 5)
      throw op[1];
    return { value: op[0] ? op[1] : void 0, done: true };
  }
};
var WebSocketTransport = (
  /** @class */
  function() {
    function WebSocketTransport2(accessTokenFactory, logger, logMessageContent) {
      this.logger = logger;
      this.accessTokenFactory = accessTokenFactory || function() {
        return null;
      };
      this.logMessageContent = logMessageContent;
    }
    WebSocketTransport2.prototype.connect = function(url, transferFormat) {
      return __awaiter5(this, void 0, void 0, function() {
        var token;
        var _this = this;
        return __generator5(this, function(_a) {
          switch (_a.label) {
            case 0:
              Arg.isRequired(url, "url");
              Arg.isRequired(transferFormat, "transferFormat");
              Arg.isIn(transferFormat, TransferFormat, "transferFormat");
              if (typeof WebSocket === "undefined") {
                throw new Error("'WebSocket' is not supported in your environment.");
              }
              this.logger.log(LogLevel.Trace, "(WebSockets transport) Connecting");
              return [4, this.accessTokenFactory()];
            case 1:
              token = _a.sent();
              if (token) {
                url += (url.indexOf("?") < 0 ? "?" : "&") + ("access_token=" + encodeURIComponent(token));
              }
              return [2, new Promise(function(resolve, reject) {
                url = url.replace(/^http/, "ws");
                var webSocket = new WebSocket(url);
                if (transferFormat === TransferFormat.Binary) {
                  webSocket.binaryType = "arraybuffer";
                }
                webSocket.onopen = function(_event) {
                  _this.logger.log(LogLevel.Information, "WebSocket connected to " + url);
                  _this.webSocket = webSocket;
                  resolve();
                };
                webSocket.onerror = function(event) {
                  reject(event.error);
                };
                webSocket.onmessage = function(message) {
                  _this.logger.log(LogLevel.Trace, "(WebSockets transport) data received. " + getDataDetail(message.data, _this.logMessageContent) + ".");
                  if (_this.onreceive) {
                    _this.onreceive(message.data);
                  }
                };
                webSocket.onclose = function(event) {
                  _this.logger.log(LogLevel.Trace, "(WebSockets transport) socket closed.");
                  if (_this.onclose) {
                    if (event.wasClean === false || event.code !== 1e3) {
                      _this.onclose(new Error("Websocket closed with status code: " + event.code + " (" + event.reason + ")"));
                    } else {
                      _this.onclose();
                    }
                  }
                };
              })];
          }
        });
      });
    };
    WebSocketTransport2.prototype.send = function(data) {
      if (this.webSocket && this.webSocket.readyState === WebSocket.OPEN) {
        this.logger.log(LogLevel.Trace, "(WebSockets transport) sending data. " + getDataDetail(data, this.logMessageContent) + ".");
        this.webSocket.send(data);
        return Promise.resolve();
      }
      return Promise.reject("WebSocket is not in the OPEN state");
    };
    WebSocketTransport2.prototype.stop = function() {
      if (this.webSocket) {
        this.webSocket.close();
        this.webSocket = null;
      }
      return Promise.resolve();
    };
    return WebSocketTransport2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/HttpConnection.js
var __awaiter6 = function(thisArg, _arguments, P, generator) {
  return new (P || (P = Promise))(function(resolve, reject) {
    function fulfilled(value) {
      try {
        step(generator.next(value));
      } catch (e) {
        reject(e);
      }
    }
    function rejected(value) {
      try {
        step(generator["throw"](value));
      } catch (e) {
        reject(e);
      }
    }
    function step(result) {
      result.done ? resolve(result.value) : new P(function(resolve2) {
        resolve2(result.value);
      }).then(fulfilled, rejected);
    }
    step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
};
var __generator6 = function(thisArg, body) {
  var _ = { label: 0, sent: function() {
    if (t[0] & 1)
      throw t[1];
    return t[1];
  }, trys: [], ops: [] }, f, y, t, g;
  return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() {
    return this;
  }), g;
  function verb(n) {
    return function(v) {
      return step([n, v]);
    };
  }
  function step(op) {
    if (f)
      throw new TypeError("Generator is already executing.");
    while (_)
      try {
        if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done)
          return t;
        if (y = 0, t)
          op = [op[0] & 2, t.value];
        switch (op[0]) {
          case 0:
          case 1:
            t = op;
            break;
          case 4:
            _.label++;
            return { value: op[1], done: false };
          case 5:
            _.label++;
            y = op[1];
            op = [0];
            continue;
          case 7:
            op = _.ops.pop();
            _.trys.pop();
            continue;
          default:
            if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) {
              _ = 0;
              continue;
            }
            if (op[0] === 3 && (!t || op[1] > t[0] && op[1] < t[3])) {
              _.label = op[1];
              break;
            }
            if (op[0] === 6 && _.label < t[1]) {
              _.label = t[1];
              t = op;
              break;
            }
            if (t && _.label < t[2]) {
              _.label = t[2];
              _.ops.push(op);
              break;
            }
            if (t[2])
              _.ops.pop();
            _.trys.pop();
            continue;
        }
        op = body.call(thisArg, _);
      } catch (e) {
        op = [6, e];
        y = 0;
      } finally {
        f = t = 0;
      }
    if (op[0] & 5)
      throw op[1];
    return { value: op[0] ? op[1] : void 0, done: true };
  }
};
var MAX_REDIRECTS = 100;
var HttpConnection = (
  /** @class */
  function() {
    function HttpConnection2(url, options) {
      if (options === void 0) {
        options = {};
      }
      this.features = {};
      Arg.isRequired(url, "url");
      this.logger = createLogger(options.logger);
      this.baseUrl = this.resolveUrl(url);
      options = options || {};
      options.accessTokenFactory = options.accessTokenFactory || function() {
        return null;
      };
      options.logMessageContent = options.logMessageContent || false;
      this.httpClient = options.httpClient || new DefaultHttpClient(this.logger);
      this.connectionState = 2;
      this.options = options;
    }
    HttpConnection2.prototype.start = function(transferFormat) {
      transferFormat = transferFormat || TransferFormat.Binary;
      Arg.isIn(transferFormat, TransferFormat, "transferFormat");
      this.logger.log(LogLevel.Debug, "Starting connection with transfer format '" + TransferFormat[transferFormat] + "'.");
      if (this.connectionState !== 2) {
        return Promise.reject(new Error("Cannot start a connection that is not in the 'Disconnected' state."));
      }
      this.connectionState = 0;
      this.startPromise = this.startInternal(transferFormat);
      return this.startPromise;
    };
    HttpConnection2.prototype.send = function(data) {
      if (this.connectionState !== 1) {
        throw new Error("Cannot send data if the connection is not in the 'Connected' State.");
      }
      return this.transport.send(data);
    };
    HttpConnection2.prototype.stop = function(error) {
      return __awaiter6(this, void 0, void 0, function() {
        var e_1;
        return __generator6(this, function(_a) {
          switch (_a.label) {
            case 0:
              this.connectionState = 2;
              _a.label = 1;
            case 1:
              _a.trys.push([1, 3, , 4]);
              return [4, this.startPromise];
            case 2:
              _a.sent();
              return [3, 4];
            case 3:
              e_1 = _a.sent();
              return [3, 4];
            case 4:
              if (!this.transport)
                return [3, 6];
              this.stopError = error;
              return [4, this.transport.stop()];
            case 5:
              _a.sent();
              this.transport = null;
              _a.label = 6;
            case 6:
              return [
                2
                /*return*/
              ];
          }
        });
      });
    };
    HttpConnection2.prototype.startInternal = function(transferFormat) {
      return __awaiter6(this, void 0, void 0, function() {
        var url, negotiateResponse, redirects, _loop_1, this_1, state_1, e_2;
        var _this = this;
        return __generator6(this, function(_a) {
          switch (_a.label) {
            case 0:
              url = this.baseUrl;
              this.accessTokenFactory = this.options.accessTokenFactory;
              _a.label = 1;
            case 1:
              _a.trys.push([1, 12, , 13]);
              if (!this.options.skipNegotiation)
                return [3, 5];
              if (!(this.options.transport === HttpTransportType.WebSockets))
                return [3, 3];
              this.transport = this.constructTransport(HttpTransportType.WebSockets);
              return [4, this.transport.connect(url, transferFormat)];
            case 2:
              _a.sent();
              return [3, 4];
            case 3:
              throw Error("Negotiation can only be skipped when using the WebSocket transport directly.");
            case 4:
              return [3, 11];
            case 5:
              negotiateResponse = null;
              redirects = 0;
              _loop_1 = function() {
                var accessToken_1;
                return __generator6(this, function(_a2) {
                  switch (_a2.label) {
                    case 0:
                      return [4, this_1.getNegotiationResponse(url)];
                    case 1:
                      negotiateResponse = _a2.sent();
                      if (this_1.connectionState === 2) {
                        return [2, { value: void 0 }];
                      }
                      if (negotiateResponse.url) {
                        url = negotiateResponse.url;
                      }
                      if (negotiateResponse.accessToken) {
                        accessToken_1 = negotiateResponse.accessToken;
                        this_1.accessTokenFactory = function() {
                          return accessToken_1;
                        };
                      }
                      redirects++;
                      return [
                        2
                        /*return*/
                      ];
                  }
                });
              };
              this_1 = this;
              _a.label = 6;
            case 6:
              return [5, _loop_1()];
            case 7:
              state_1 = _a.sent();
              if (typeof state_1 === "object")
                return [2, state_1.value];
              _a.label = 8;
            case 8:
              if (negotiateResponse.url && redirects < MAX_REDIRECTS)
                return [3, 6];
              _a.label = 9;
            case 9:
              if (redirects === MAX_REDIRECTS && negotiateResponse.url) {
                throw Error("Negotiate redirection limit exceeded.");
              }
              return [4, this.createTransport(url, this.options.transport, negotiateResponse, transferFormat)];
            case 10:
              _a.sent();
              _a.label = 11;
            case 11:
              if (this.transport instanceof LongPollingTransport) {
                this.features.inherentKeepAlive = true;
              }
              this.transport.onreceive = this.onreceive;
              this.transport.onclose = function(e) {
                return _this.stopConnection(e);
              };
              this.changeState(
                0,
                1
                /* Connected */
              );
              return [3, 13];
            case 12:
              e_2 = _a.sent();
              this.logger.log(LogLevel.Error, "Failed to start the connection: " + e_2);
              this.connectionState = 2;
              this.transport = null;
              throw e_2;
            case 13:
              return [
                2
                /*return*/
              ];
          }
        });
      });
    };
    HttpConnection2.prototype.getNegotiationResponse = function(url) {
      return __awaiter6(this, void 0, void 0, function() {
        var _a, token, headers, negotiateUrl, response, e_3;
        return __generator6(this, function(_b) {
          switch (_b.label) {
            case 0:
              return [4, this.accessTokenFactory()];
            case 1:
              token = _b.sent();
              if (token) {
                headers = (_a = {}, _a["Authorization"] = "Bearer " + token, _a);
              }
              negotiateUrl = this.resolveNegotiateUrl(url);
              this.logger.log(LogLevel.Debug, "Sending negotiation request: " + negotiateUrl);
              _b.label = 2;
            case 2:
              _b.trys.push([2, 4, , 5]);
              return [4, this.httpClient.post(negotiateUrl, {
                content: "",
                headers
              })];
            case 3:
              response = _b.sent();
              if (response.statusCode !== 200) {
                throw Error("Unexpected status code returned from negotiate " + response.statusCode);
              }
              return [2, JSON.parse(response.content)];
            case 4:
              e_3 = _b.sent();
              this.logger.log(LogLevel.Error, "Failed to complete negotiation with the server: " + e_3);
              throw e_3;
            case 5:
              return [
                2
                /*return*/
              ];
          }
        });
      });
    };
    HttpConnection2.prototype.createConnectUrl = function(url, connectionId) {
      return url + (url.indexOf("?") === -1 ? "?" : "&") + ("id=" + connectionId);
    };
    HttpConnection2.prototype.createTransport = function(url, requestedTransport, negotiateResponse, requestedTransferFormat) {
      return __awaiter6(this, void 0, void 0, function() {
        var connectUrl, transports, _i, transports_1, endpoint, transport, ex_1;
        return __generator6(this, function(_a) {
          switch (_a.label) {
            case 0:
              connectUrl = this.createConnectUrl(url, negotiateResponse.connectionId);
              if (!this.isITransport(requestedTransport))
                return [3, 2];
              this.logger.log(LogLevel.Debug, "Connection was provided an instance of ITransport, using that directly.");
              this.transport = requestedTransport;
              return [4, this.transport.connect(connectUrl, requestedTransferFormat)];
            case 1:
              _a.sent();
              this.changeState(
                0,
                1
                /* Connected */
              );
              return [
                2
                /*return*/
              ];
            case 2:
              transports = negotiateResponse.availableTransports;
              _i = 0, transports_1 = transports;
              _a.label = 3;
            case 3:
              if (!(_i < transports_1.length))
                return [3, 9];
              endpoint = transports_1[_i];
              this.connectionState = 0;
              transport = this.resolveTransport(endpoint, requestedTransport, requestedTransferFormat);
              if (!(typeof transport === "number"))
                return [3, 8];
              this.transport = this.constructTransport(transport);
              if (!(negotiateResponse.connectionId === null))
                return [3, 5];
              return [4, this.getNegotiationResponse(url)];
            case 4:
              negotiateResponse = _a.sent();
              connectUrl = this.createConnectUrl(url, negotiateResponse.connectionId);
              _a.label = 5;
            case 5:
              _a.trys.push([5, 7, , 8]);
              return [4, this.transport.connect(connectUrl, requestedTransferFormat)];
            case 6:
              _a.sent();
              this.changeState(
                0,
                1
                /* Connected */
              );
              return [
                2
                /*return*/
              ];
            case 7:
              ex_1 = _a.sent();
              this.logger.log(LogLevel.Error, "Failed to start the transport '" + HttpTransportType[transport] + "': " + ex_1);
              this.connectionState = 2;
              negotiateResponse.connectionId = null;
              return [3, 8];
            case 8:
              _i++;
              return [3, 3];
            case 9:
              throw new Error("Unable to initialize any of the available transports.");
          }
        });
      });
    };
    HttpConnection2.prototype.constructTransport = function(transport) {
      switch (transport) {
        case HttpTransportType.WebSockets:
          return new WebSocketTransport(this.accessTokenFactory, this.logger, this.options.logMessageContent);
        case HttpTransportType.ServerSentEvents:
          return new ServerSentEventsTransport(this.httpClient, this.accessTokenFactory, this.logger, this.options.logMessageContent);
        case HttpTransportType.LongPolling:
          return new LongPollingTransport(this.httpClient, this.accessTokenFactory, this.logger, this.options.logMessageContent);
        default:
          throw new Error("Unknown transport: " + transport + ".");
      }
    };
    HttpConnection2.prototype.resolveTransport = function(endpoint, requestedTransport, requestedTransferFormat) {
      var transport = HttpTransportType[endpoint.transport];
      if (transport === null || transport === void 0) {
        this.logger.log(LogLevel.Debug, "Skipping transport '" + endpoint.transport + "' because it is not supported by this client.");
      } else {
        var transferFormats = endpoint.transferFormats.map(function(s) {
          return TransferFormat[s];
        });
        if (transportMatches(requestedTransport, transport)) {
          if (transferFormats.indexOf(requestedTransferFormat) >= 0) {
            if (transport === HttpTransportType.WebSockets && typeof WebSocket === "undefined" || transport === HttpTransportType.ServerSentEvents && typeof EventSource === "undefined") {
              this.logger.log(LogLevel.Debug, "Skipping transport '" + HttpTransportType[transport] + "' because it is not supported in your environment.'");
            } else {
              this.logger.log(LogLevel.Debug, "Selecting transport '" + HttpTransportType[transport] + "'");
              return transport;
            }
          } else {
            this.logger.log(LogLevel.Debug, "Skipping transport '" + HttpTransportType[transport] + "' because it does not support the requested transfer format '" + TransferFormat[requestedTransferFormat] + "'.");
          }
        } else {
          this.logger.log(LogLevel.Debug, "Skipping transport '" + HttpTransportType[transport] + "' because it was disabled by the client.");
        }
      }
      return null;
    };
    HttpConnection2.prototype.isITransport = function(transport) {
      return transport && typeof transport === "object" && "connect" in transport;
    };
    HttpConnection2.prototype.changeState = function(from, to) {
      if (this.connectionState === from) {
        this.connectionState = to;
        return true;
      }
      return false;
    };
    HttpConnection2.prototype.stopConnection = function(error) {
      return __awaiter6(this, void 0, void 0, function() {
        return __generator6(this, function(_a) {
          this.transport = null;
          error = this.stopError || error;
          if (error) {
            this.logger.log(LogLevel.Error, "Connection disconnected with error '" + error + "'.");
          } else {
            this.logger.log(LogLevel.Information, "Connection disconnected.");
          }
          this.connectionState = 2;
          if (this.onclose) {
            this.onclose(error);
          }
          return [
            2
            /*return*/
          ];
        });
      });
    };
    HttpConnection2.prototype.resolveUrl = function(url) {
      if (url.lastIndexOf("https://", 0) === 0 || url.lastIndexOf("http://", 0) === 0) {
        return url;
      }
      if (typeof window === "undefined" || !window || !window.document) {
        throw new Error("Cannot resolve '" + url + "'.");
      }
      var aTag = window.document.createElement("a");
      aTag.href = url;
      this.logger.log(LogLevel.Information, "Normalizing '" + url + "' to '" + aTag.href + "'.");
      return aTag.href;
    };
    HttpConnection2.prototype.resolveNegotiateUrl = function(url) {
      var index = url.indexOf("?");
      var negotiateUrl = url.substring(0, index === -1 ? url.length : index);
      if (negotiateUrl[negotiateUrl.length - 1] !== "/") {
        negotiateUrl += "/";
      }
      negotiateUrl += "negotiate";
      negotiateUrl += index === -1 ? "" : url.substring(index);
      return negotiateUrl;
    };
    return HttpConnection2;
  }()
);
function transportMatches(requestedTransport, actualTransport) {
  return !requestedTransport || (actualTransport & requestedTransport) !== 0;
}

// node_modules/@aspnet/signalr/dist/esm/JsonHubProtocol.js
var JSON_HUB_PROTOCOL_NAME = "json";
var JsonHubProtocol = (
  /** @class */
  function() {
    function JsonHubProtocol2() {
      this.name = JSON_HUB_PROTOCOL_NAME;
      this.version = 1;
      this.transferFormat = TransferFormat.Text;
    }
    JsonHubProtocol2.prototype.parseMessages = function(input, logger) {
      if (typeof input !== "string") {
        throw new Error("Invalid input for JSON hub protocol. Expected a string.");
      }
      if (!input) {
        return [];
      }
      if (logger === null) {
        logger = NullLogger.instance;
      }
      var messages = TextMessageFormat.parse(input);
      var hubMessages = [];
      for (var _i = 0, messages_1 = messages; _i < messages_1.length; _i++) {
        var message = messages_1[_i];
        var parsedMessage = JSON.parse(message);
        if (typeof parsedMessage.type !== "number") {
          throw new Error("Invalid payload.");
        }
        switch (parsedMessage.type) {
          case MessageType.Invocation:
            this.isInvocationMessage(parsedMessage);
            break;
          case MessageType.StreamItem:
            this.isStreamItemMessage(parsedMessage);
            break;
          case MessageType.Completion:
            this.isCompletionMessage(parsedMessage);
            break;
          case MessageType.Ping:
            break;
          case MessageType.Close:
            break;
          default:
            logger.log(LogLevel.Information, "Unknown message type '" + parsedMessage.type + "' ignored.");
            continue;
        }
        hubMessages.push(parsedMessage);
      }
      return hubMessages;
    };
    JsonHubProtocol2.prototype.writeMessage = function(message) {
      return TextMessageFormat.write(JSON.stringify(message));
    };
    JsonHubProtocol2.prototype.isInvocationMessage = function(message) {
      this.assertNotEmptyString(message.target, "Invalid payload for Invocation message.");
      if (message.invocationId !== void 0) {
        this.assertNotEmptyString(message.invocationId, "Invalid payload for Invocation message.");
      }
    };
    JsonHubProtocol2.prototype.isStreamItemMessage = function(message) {
      this.assertNotEmptyString(message.invocationId, "Invalid payload for StreamItem message.");
      if (message.item === void 0) {
        throw new Error("Invalid payload for StreamItem message.");
      }
    };
    JsonHubProtocol2.prototype.isCompletionMessage = function(message) {
      if (message.result && message.error) {
        throw new Error("Invalid payload for Completion message.");
      }
      if (!message.result && message.error) {
        this.assertNotEmptyString(message.error, "Invalid payload for Completion message.");
      }
      this.assertNotEmptyString(message.invocationId, "Invalid payload for Completion message.");
    };
    JsonHubProtocol2.prototype.assertNotEmptyString = function(value, errorMessage) {
      if (typeof value !== "string" || value === "") {
        throw new Error(errorMessage);
      }
    };
    return JsonHubProtocol2;
  }()
);

// node_modules/@aspnet/signalr/dist/esm/HubConnectionBuilder.js
var HubConnectionBuilder = (
  /** @class */
  function() {
    function HubConnectionBuilder2() {
    }
    HubConnectionBuilder2.prototype.configureLogging = function(logging) {
      Arg.isRequired(logging, "logging");
      if (isLogger(logging)) {
        this.logger = logging;
      } else {
        this.logger = new ConsoleLogger(logging);
      }
      return this;
    };
    HubConnectionBuilder2.prototype.withUrl = function(url, transportTypeOrOptions) {
      Arg.isRequired(url, "url");
      this.url = url;
      if (typeof transportTypeOrOptions === "object") {
        this.httpConnectionOptions = transportTypeOrOptions;
      } else {
        this.httpConnectionOptions = {
          transport: transportTypeOrOptions
        };
      }
      return this;
    };
    HubConnectionBuilder2.prototype.withHubProtocol = function(protocol) {
      Arg.isRequired(protocol, "protocol");
      this.protocol = protocol;
      return this;
    };
    HubConnectionBuilder2.prototype.build = function() {
      var httpConnectionOptions = this.httpConnectionOptions || {};
      if (httpConnectionOptions.logger === void 0) {
        httpConnectionOptions.logger = this.logger;
      }
      if (!this.url) {
        throw new Error("The 'HubConnectionBuilder.withUrl' method must be called before building the connection.");
      }
      var connection = new HttpConnection(this.url, httpConnectionOptions);
      return HubConnection.create(connection, this.logger || NullLogger.instance, this.protocol || new JsonHubProtocol());
    };
    return HubConnectionBuilder2;
  }()
);
function isLogger(logger) {
  return logger.log !== void 0;
}

// node_modules/@aspnet/signalr/dist/esm/index.js
var VERSION = "1.0.27";
export {
  DefaultHttpClient,
  HttpClient,
  HttpError,
  HttpResponse,
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
  JsonHubProtocol,
  LogLevel,
  MessageType,
  NullLogger,
  TimeoutError,
  TransferFormat,
  VERSION
};
//# sourceMappingURL=@aspnet_signalr.js.map
