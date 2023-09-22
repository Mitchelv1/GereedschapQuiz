mergeInto(LibraryManager.library, {

  GetJSON: function (path, objectName, callback, fallback) {
	var parsedPath = Pointer_stringify(path);
	var parsedObjectName = Pointer_stringify(objectName);
	var parsedCallback = Pointer_stringify(callback);
	var parsedFallback = Pointer_stringify(fallback);
	
	try {
	
	firebase.database().ref(parsedPath).once('value').then(function(snapshot) {
		window.unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot.val()));
	});
	
	} catch (error) {
		window.unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, "There was an error: " + error.message);
	}
  },

  PostJSON: function (path, value, objectName, callback, fallback) {
	var parsedPath = Pointer_stringify(path);
	var parsedValue = Pointer_stringify(value);
	var parsedObjectName = Pointer_stringify(objectName);
	var parsedCallback = Pointer_stringify(callback);
	var parsedFallback = Pointer_stringify(fallback);
	
	try {
	
	firebase.database().ref(parsedPath).set(parsedValue).then(function(unused) {
		window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "Success: " + parsedValue + " was posted to " + parsedPath);
	});
	
	} catch (error) {
		window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
	}
  },
});