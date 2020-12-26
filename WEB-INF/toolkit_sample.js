"use strict";
var ToolkitOB = null;
var readerClass = null;
this.fingerData = null;
this.verifyxmldata = null;
var self = this;
var javaService = '';
this.IsNfc = false;
var localAddress = window.location.href;
var options = {
		"jnlp_address": javaService + "IDCardToolkitService.jnlp",
		"debugEnabled": true,
		"agent_tls_enabled": false
	};
var IsSam = {
	sam_secure_messaging: false
};
options.toolkitConfig = 'vg_url = http://vg-pre-prod.ica.gov.ae/ValidationGatewayService \n'
options.toolkitConfig += 'vg_connection_timeout = 60 \n';
//options.toolkitConfig += 'config_url =' + localAddress + 'config \n';
//options.toolkitConfig += 'environment = dev \n';
options.toolkitConfig += 'log_level = "INFO" \n';
options.toolkitConfig += 'log_performance_time = true \n'
options.toolkitConfig += 'read_publicdata_offline = true \n';

var signingContext = {
	signatureLevel: null,
	packagingMode: null,
	digestAlgorithm: null,
	userPin: null,
	tsaUrl: null,
	ocspUrl: null,
	certPath: null,
	countryCode: null,
	locality: null,
	postalCode: null,
	stateOrProvince: null,
	street: null,
	signNmPositionSelect: null,
	sigVisibleSelect: null,
	pgNumberTxtBx: null,
	sigTextTxtBx: null,
	fontNameTxtBx: null,
	fontSizeTxtBx: null,
	fontColorTxtBx: null,
	bgColorTxtBx: null,
	sigImgPathTxtBx: null,
	sigYaxisTxtBx: null,
	sigXaxisTxtBx: null,
	signerContactInfoTxtBx: null,
	signerLocationTxtBx: null,
	reasonSignTxtBx: null
};
var verificationContext = {
	inputPath: null,
	packagingMode: null,
	certPath: null,
	signedData: null,
	detached: null,
	detachedValue: null
};
/**
 * Error handler call back function.
 * This function is executed if any error occurred in the web socket communication.
 * This function is passed as a error call back function while initializing the web socket.
 * 
 * @param err error details
 */
var errorHandlerCB = function (err) {
	readerClass = null;
	ToolkitOB = null;
	if (null !== err) {
		// hideLoader();
		alert('errorHandler ERROR : ' + err);
	}
}
/**
 * Close handler call back function.
 * This function is executed when web socket connection is closed.
 * This function is passed as a close call back function while initializing the web socket.
 * 
 * @param event event details
 */
var closeHandlerCB = function (event) {
	// hideLoader();
	ToolkitOB = null;
	readerClass = null;
	if (null !== event && undefined == event) {}
	changeButtonState(true);
	document.getElementById("workAreaDiv").style.display = 'none';
}
/**
 * Close handler call back function.
 * This function is executed when web socket connection is closed.
 * This function is passed as a close call back function while initializing the web socket.
 * 
 * @param event event details
 */
/**
 * open handler call back function.
 * This function is executed when web socket connection is opened/established successfully.
 * This function is passed as a onOpen call back function while initializing the web socket.
 *  
 */
var onOpenHandlerCB = function (response, error) {
	// hideLoader();
	if (error === null) {
		/**
		 * call the list reader function and pass listReaderCB to be executed
		 * after the response is received from server
		 */
		if (IsSam.sam_secure_messaging) {
			ToolkitOB.getReaderWithEmiratesId(listReaderCB);
		} else {
			ToolkitOB.listReaders(listReaderCB);
		}
	} else {
		ToolkitOB = null;
	}
}
/**
 * listReader handler call back function.
 * This function is executed when response is received from server for listReader request.
 * This function is passed as a listReader call back function while sending listReader request.
 * 
 * @param response describing response from server.
 *  response object has one field 'data' which contains a string in the json format.
 *  This string can be converted to json object by using JSON.parse(response.data);
 * 
 */
var listReaderCB = function (response, error) {
	if (error !== null) {
		alert(error.message || error.description);
		ToolkitOB = null;
	} else {
		var readerName = null;
		var readerList = response;
		if (IsSam.sam_secure_messaging) {
			readerClass = readerList;
		} else {
			if (readerList && 0 < readerList.length) {
				readerClass = readerList[0];
			} else {
				return 'No readers found';
			}
		}
		displayProgress('Connecting to reader ...');
		/**
		 * call the connect reader function and pass connectReaderCB to be executed
		 * after the response is received from server
		 */
		var ret = readerClass.connect(connectReaderCB);
		if ('' !== ret) {
			/* disable all buttons till request is processed */
			changeButtonState(true);
		}
	}
}
/**
 * connectReader handler call back function.
 * This function is executed when response is received from server for connectReader request.
 * This function is passed as a connectReader call back function while sending connectReader request.
 * 
 * @param response describing response from server.
 *  response object has one field 'data' which contains a string in the json format.
 *  This string can be converted to json object by using JSON.parse(response.data);
 * 
 */
var connectReaderCB = function (response, error) {
	if (null != error) {
		alert(error.code + ' : ' + error.message);
		ToolkitOB = null;
		return;
	}
	readerClass.getInterfaceType(getInterfaceCB);
	document.getElementById("workAreaDiv").style.display = 'block';
	displayProgress('Getting Interface  ...');
}

/**
 * This function is used to get Interface of the reader
 */
var getInterfaceCB = function (response, error) {
	if (null != error) {
		alert(error.code + ' : ' + error.message);
		ToolkitOB = null;
		return;
	}
	if (response === 2) {
		self.IsNfc = true;
	} else {
		self.IsNfc = false;
	}
	displayProgress('Web socket connected ...');
	/* enable all buttons */
	changeButtonState(false);
}
/**
 * This function is used to initialize the PublicDataWebComponent
 */
function Initialize() {
	try {
		// showLoader();
		/* Ensures only one connection is open at a time */
		if (ToolkitOB !== null) {
			/*  enable all buttons  */
			changeButtonState(false);
			hideLoader();
			return 'WebSocket is already active...';
		}
		/*  if
		 provide the call backs */
		ToolkitOB = new Toolkit(
			onOpenHandlerCB, /* reference to onOpen call back function */
			closeHandlerCB, /* reference to onClose call back function */
			errorHandlerCB, /* reference to onError call back function */
			options /* options */
		);
		displayProgress('Initializing web socket ...');
	} catch (e) {
		// hideLoader();
		alert("Webcomponent Initialization Failed, Details: " + e);
	}
}
/**
 * This function is used to read the public data from first reader
 * found.
 */
function DisplayPublicData(nfc) {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Reader is not initiaized.');
		return;
	}
	/*  disable all buttons till request is processed */
	changeButtonState(true);
	showLoader();
	displayProgress('Reading public data...');
	/*  generate the random string */
	var randomStr = generateRandomString(40);
	/* convert randomString to base64 */
	var requestId = btoa(randomStr);
	/**
	 * call the read public data function and pass readPublicDataCB to be executed
	 * after the response is received from server
	 */
	var address = true;
	if (self.IsNfc) {
		address = false;
	}
	readerClass.readPublicData(
		requestId,
		true,
		true,
		true,
		true,
		address,
		readPublicDataCB);
	changeButtonState(true);
}
/**
 * readPublicData handler call back function.
 * This function is executed when response is received from server for readPublicData request.
 * This function is passed as a readPublicData call back function while sending readPublicData request.
 * 
 * @param response describing response from server.
 *  response object has one field 'data' which contains a string in the json format.
 *  This string can be converted to json object by using JSON.parse(response.data);
 * 
 */
var readPublicDataCB = function (response, error) {
	hideLoader();
	if (error === null) {
		displayData(response, 'poublicDataDiv');
		if (!self.IsNfc) {
			setVerifyXml('poublicDataDiv', response.xmlString);
		}

	} else {
		alert(error.message);
	}
	/* enable all buttons as request is completed */
	changeButtonState(false);
}

/**
 * This function is to check card status
 */
function CheckCardStatus() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Reader is not initiaized.');
		return;
	}
	displayProgress('Checking Card Status...');
	showLoader();
	var randomStr = generateRandomString(40);
	var requestId = btoa(randomStr);
	readerClass.checkCardStatus(requestId, CheckCardStatusCB);
	changeButtonState(true);
}
var CheckCardStatusCB = function (response, error) {
	showDiv("cardStatusDiv");
	hideLoader();
	if (error !== null) {
		document.getElementById("cardStatusTxtBx").style.color = "red";
		document.getElementById("cardStatusTxtBx").value = "InValid :" + error.message || 'card';
		return;
	}
	setVerifyXml('cardStatusDiv', response.xmlString);
	document.getElementById("cardStatusTxtBx").style.color = "green";
	document.getElementById("cardStatusTxtBx").value = "Card Is Valid";
	document.getElementById("cardStatusTxtXMlrow").style.display = null;
	document.getElementById("cardStatusTxtXML").value = response.xmlString;
	changeButtonState(false);
}
/**
 * This function is to read certificates
 */
function ReadCertificate() {
	showDiv("readCertsDiv");
}

function ReadCertificates() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Reader is not initiaized.');
		return;
	}
	changeButtonState(true);
	var pin = document.getElementById('rd_cert_pin').value;
	if (pin == null || undefined == pin || '' == pin || pin.length < 4) {
		alert('Please enter valid pin.');
		return;
	}
	showLoader();
	PrepareRequest(function (requestHandle) {
		if (requestHandle === undefined || requestHandle === null) {
			var encodedPin = pin;
			readerClass.getPkiCertificates(encodedPin, ReadCertificatesCB);
		} else {
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
			var encodedPin = encodePinOnServer(pin, requestHandle, response.publicKey);
			if (encodedPin == -1) {
				hideLoader();
				changeButtonState(false);
				alert('Failed to Encrypt data');
				return;
			}
			
			readerClass.getPkiCertificates(encodedPin, ReadCertificatesCB);
		})
		}
	});
}
var ReadCertificatesCB = function (response, error) {
	hideLoader();
	if (null !== error) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		return;
	}
	var result = response;
	setVerifyXml('readCertsDiv', result.xmlString);
	if ('fail' === result.status) {
		return result.error + ' : ' + result.description;
	}
	document.getElementById("signCertTextArea").value = result.signingCertificate;
	document.getElementById("authCertTextArea").value = result.authenticationCertificate;
	/*  enable all buttons as request is completed */
	changeButtonState(false);
}
/**
 * This function is to get finger indexes
 */
function GetFingerIndex() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Reader is not initiaized.');
		return;
	}
	showDiv("fingerIndexDiv");
	showLoader();
	changeButtonState(true);
	readerClass.getFingerData(GetFingerIndexCB)
	changeButtonState(true);
	return;
}
var GetFingerIndexCB = function (response, error) {
	hideLoader();
	if (null != error) {
		alert(error.message);
		return;
	}
	document.getElementById("fingerIndexTextArea").value = response[0].fingerIndex + "\n" + response[1].fingerIndex;
	changeButtonState(false);
}
/**
 * This function is to verify biometric
 */
function VerifyBio() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Reader is not initiaized.');
		return;
	}
	showDiv("verifyBioDiv");
	changeButtonState(true);
	showLoader();
	readerClass.getFingerData(
		function (event, error) {
			hideLoader();
			if (error !== null) {
				alert(error.message);
				changeButtonState(true);
				return;
			}
			var result = event;
			if ('fail' === result.status) {
				return result.error + ' : ' + result.description;
			}
			/* set result of getFingerIndex to local variable so that it can be while verifying biometric */
			self.fingerData = result;
			var selectBox = document.getElementById("verifyBioFingerSelect");
			if (selectBox.options.length > 1) {
				selectBox.removeChild(selectBox.options[2]);
				selectBox.removeChild(selectBox.options[1]);
			}
			var option1 = document.createElement("option");
			var opt1 = result[0].fingerIndex;
			option1.text = opt1;
			selectBox.add(option1);
			var option2 = document.createElement("option");
			option2.text = result[1].fingerIndex;
			selectBox.add(option2);
			changeButtonState(false);
		})
}

function VerifyBioSubmit() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	var selectedFinger = document.getElementById("verifyBioFingerSelect").value
	if ('' == selectedFinger || undefined == selectedFinger) {
		alert('Please select a finger.');
		return;
	}
	/*  disable all buttons till request is processe */
	changeButtonState(true);
	showLoader();
	displayProgress('Verifying biometric ...');
	var sensor_timeout = 30; /* seconds */
	var randomStr = generateRandomString(40);
	var requestId = btoa(randomStr);
	readerClass.authenticateBiometricOnServer(requestId, selectedFinger, sensor_timeout, VerifyBioCB);
}
var VerifyBioCB = function (event, error) {
	hideLoader();
	if (null != error) {
		changeButtonState(false);
		alert(error.message);
		return;
	}
	result = event;
	setVerifyXml('verifyBioDiv', result.xmlString);
	changeButtonState(true);
	document.getElementById("verifyBioTxtBx").style.color = "green";
	document.getElementById("verifyBioTxtBx").value = "Successful.";
	document.getElementById("verifyBioTxtBx").type = "text";
	/* disable all buttons till request is processed */
	changeButtonState(false);
}

function PKIAuth() {
	showDiv("pkiAuthDiv");
}

function PKIAuthSubmit() {
	var pin = document.getElementById("pkiAuthTxtBx").value;
	if (pin == null || undefined == pin || '' == pin || pin.length < 4) {
		alert('Please enter valid pin.');
		return;
	}
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	changeButtonState(true);
	showLoader();
	PrepareRequest(function (requestHandle) {
		if (requestHandle === undefined || requestHandle === null) {
			var encodedPin = pin;
			readerClass.authenticatePki(encodedPin, PKIAuthCB);
		} else {
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
			var encodedPin = encodePinOnServer(pin, requestHandle, response.publicKey);
			if (encodedPin == -1) {
				hideLoader();
				changeButtonState(false);
				alert('Failed to Encrypt data');
				return;
			}
			readerClass.authenticatePki(encodedPin, PKIAuthCB);
			})
		}
	});
}
var PKIAuthCB = function (response, error) {
	hideLoader();
	changeButtonState(false);
	if (error !== null) {
		document.getElementById("pkiAuthResultTxtBx").type = "text";
		document.getElementById("pkiAuthResultTxtBx").style.color = "red";
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		document.getElementById("pkiAuthResultTxtBx").value = error.message;
		return;
	}
	setVerifyXml('pkiAuthDiv', response.xmlString);
	document.getElementById("pkiAuthResultTxtBx").type = "text";
	document.getElementById("pkiAuthResultTxtBx").style.color = "green";
	document.getElementById("pkiAuthResultTxtBx").value = "Valid Auth Cert";
}
/**
 * This function is to show sign data div
 */
function SignData() {
	showDiv("signDataDiv");
}
/**
 * This function is to sign data
 */
function SignDataSubmit() {
	var dataHashed = document.getElementById("signDataHashedSelect").value;
	if (undefined === dataHashed || '' === dataHashed) {
		alert('Please select type of data.');
		return;
	}
	var data = document.getElementById("dataTextArea").value;
	if (undefined === data || '' === data) {
		alert('Please enter valid data.');
		return;
	}
	var pin = prompt("Please enter your pin", "");
	if (pin == null || undefined == pin || '' == pin || pin.length < 4) {
		alert('Please enter valid pin.');
		return;
	}
	var result = null;
	var readerName = null;
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	/* disable all buttons till request is processed */
	changeButtonState(true);
	showLoader();
	PrepareRequest(function (requestHandle) {
		if (requestHandle === undefined || requestHandle === null) {
			var encodedPin = pin;
			readerClass.signData(data, parseInt(dataHashed), encodedPin, SignDataCB);
		} else {
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
			var encodedPin = encodePinOnServer(pin, requestHandle,response.publicKey);
			if (encodedPin == -1) {
				hideLoader();
				changeButtonState(false);
				alert('Failed to Encrypt data');
				return;
			}
			readerClass.signData(data, parseInt(dataHashed), encodedPin, SignDataCB);
					})
		}
	});
}
var SignDataCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		return;
	}
	var result = response;
	setVerifyXml('signDataDiv', result.xmlString);
	var resultData = '';
	if ('fail' === result.status) {
		resultData = result.error + ' : ' + result.description + ' : ' + result.attemptsLeft;
		document.getElementById("resultTextArea").value = resultData;
		changeButtonState(false);
		return;
	}
	resultData = result.signature;
	document.getElementById("resultTextArea").value = resultData;
	changeButtonState(false);
}

function SignChallengeData() {
	showDiv("signChallangeDataDiv");
}
/**
 * This function is to sign data
 */
function SignChallengeDataSubmit() {
	var dataHashed = document.getElementById("challengesignDataHashedSelect").value;
	if (undefined === dataHashed || '' === dataHashed) {
		alert('Please select type of data.');
		return;
	}
	var data = document.getElementById("challengedataTextArea").value;
	if (undefined === data || '' === data) {
		alert('Please enter valid data.');
		return;
	}
	var pin = prompt("Please enter your pin", "");
	if (pin == null || undefined == pin || '' == pin || pin.length < 4) {
		alert('Please enter valid pin.');
		return;
	}
	var result = null;
	var readerName = null;
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	/*  disable all buttons till request is processed */
	changeButtonState(true);
	showLoader();
	PrepareRequest(function (requestHandle) {
		if (requestHandle === undefined || requestHandle === null) {
			var encodedPin = pin;
			readerClass.signChallenge(data, parseInt(dataHashed), encodedPin, SignChallangeDataCB);
		} else {
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
			var encodedPin = encodePinOnServer(pin, requestHandle,response.publicKey);
			if (encodedPin == -1) {
				hideLoader();
				changeButtonState(false);
				alert('Failed to Encrypt data');
				return;
			}
			readerClass.signChallenge(data, parseInt(dataHashed), encodedPin, SignChallangeDataCB);
					})
		}
	});
}
var SignChallangeDataCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		return;
	}
	var result = response;
	setVerifyXml('signChallangeDataDiv', result.xmlString);
	document.getElementById("challengeresultTextArea").value = result.signature;
	/* enable all buttons as request is processed */
	changeButtonState(false);
}
/**
 * This function is to sign data
 */
function VerifySignature() {
	showDiv("verifyDataDiv");
}

function VerifyDataSubmit() {
	var dataHashed = document.getElementById("verifyDataHashedSelect").value;
	if (undefined === dataHashed || '' === dataHashed) {
		alert('Please select type of data.');
		return;
	}
	var certType = document.getElementById("verifyDataCertSelect").value;
	if (undefined === certType || '' === certType) {
		alert('Please select type of data.');
		return;
	}
	var originalData = document.getElementById("originalDataTextArea").value;
	if (undefined === originalData || '' === originalData) {
		alert('Please enter valid original data.');
		return;
	}
	var signedData = document.getElementById("signedDataTextArea").value;
	if (undefined === signedData || '' === signedData) {
		alert('Please enter valid signed data.');
		return;
	}
	var pin = prompt("Please enter your pin", "");
	if (pin == null || undefined == pin || '' == pin || pin.length < 4) {
		alert('Please enter valid pin.');
		return;
	}
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	var certData = "";
	showLoader();
	PrepareRequest(function (requestHandle) {
		if (requestHandle === undefined || requestHandle === null) {
			var encodedPin = pin;
			readerClass.getPkiCertificates(encodedPin, function (event, error) {
				if (certType == 1) {
					certData = event.signingCertificate;
				} else {
					certData = event.authenticationCertificate;
				}
				changeButtonState(true);
				readerClass.verifySignature(originalData, parseInt(dataHashed), signedData, certData, VerifyDataCB);
				// changeButtonState(true);
			});
		} else {
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
			var encodedPin = encodePinOnServer(pin, requestHandle, response.publicKey);
			if (encodedPin == -1) {
				hideLoader();
				changeButtonState(false);
				alert('Failed to Encrypt data');
				return;
			}
			readerClass.getPkiCertificates(encodedPin, function (event, error) {
				if (certType == 1) {
					certData = event.signingCertificate;
				} else {
					certData = event.authenticationCertificate;
				}
				changeButtonState(true);
				readerClass.verifySignature(originalData, parseInt(dataHashed), signedData, certData, VerifyDataCB);
			})
				// changeButtonState(true);
			});
		}

	});
	/* disable all buttons till request is processed */

}
var VerifyDataCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		document.getElementById("verifyDataTxtBx").value = error.message;
		document.getElementById("verifyDataTxtBx").type = "text";
		document.getElementById("verifyDataTxtBx").style.color = "red";
		return;
	}
	var resultData = "Verification Successful."
	document.getElementById("verifyDataTxtBx").value = resultData;
	document.getElementById("verifyDataTxtBx").type = "text";
	document.getElementById("verifyDataTxtBx").style.color = "green";
	changeButtonState(false);
}
/**
 * This function is to reset pin
 */
function pinReset() {
	showDiv("pinResetDiv");
	var result = null;
	var readerName = null;
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	/* disable all buttons till request is processed */
	changeButtonState(true);
	displayProgress('Getting finger indexes...');
	readerClass.getFingerData(
		function (event, error) {
			if (error !== null) {
				alert(error.message);
				changeButtonState(true);
				return;
			}
			var result = event;
			if ('fail' === result.status) {
				return result.error + ' : ' + result.description;
			}
			/* set result of getFingerIndex to local variable so that it can be while verifying biometric */
			self.fingerData = result;
			var selectBox = document.getElementById("resetPINFingerSelect");
			if (selectBox.options.length > 1) {
				selectBox.removeChild(selectBox.options[2]);
				selectBox.removeChild(selectBox.options[1]);
			}
			var option1 = document.createElement("option");
			var opt1 = result[0].fingerIndex;
			option1.text = opt1;
			selectBox.add(option1);
			var option2 = document.createElement("option");
			option2.text = result[1].fingerIndex;
			selectBox.add(option2);
			changeButtonState(false);
		})
}
/**
 * This function is to verify biometric
 */
function pinResetSubmit() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	var pin = document.getElementById("pinResetTxtBx").value;
	if (undefined === pin || '' === pin || pin.length < 4) {
		alert('Please provide valid pin .');
		return;
	}
	var selectedFinger = document.getElementById("resetPINFingerSelect").value
	if ('' == selectedFinger || undefined == selectedFinger) {
		alert('Please select a finger.');
		return;
	}
	/* disable all buttons till request is processed */
	changeButtonState(true);
	displayProgress('Resetting PIN ...');

	var index = 0;
	var indexId = 0;
	/* get finger index from selectedFinger */
	for (let i = 0; i < self.fingerData.length; i++) {
		if (self.fingerData[i].fingerIndex === selectedFinger) {
			index = self.fingerData[i];
			break;
		}
	}
	var sensor_timeout = 30; /*  seconds */
	showLoader();
	PrepareRequest(function (requestHandle) {
		if (requestHandle === undefined || requestHandle === null) {
			var encodedPin = pin;
			readerClass.resetPin(encodedPin, index, sensor_timeout, pinResetCB);
		} else {
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
			var encodedPin = encodePinOnServer(pin, requestHandle, response.publicKey);
			if (encodedPin == -1) {
				hideLoader();
				changeButtonState(false);
				alert('Failed to Encrypt data');
				return;
			}
			readerClass.resetPin(encodedPin, index, sensor_timeout, pinResetCB);
					})
		}
	});
}
var pinResetCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		return;
	}
	result = response;
	/* check if there is any error in response */
	document.getElementById("pinResetBioTxtBx").style.color = "green";
	document.getElementById("pinResetBioTxtBx").value = "Successful.";
	document.getElementById("pinResetBioTxtBx").type = "text";
	/* enable all buttons as request is processed */
	changeButtonState(false);
}
/**
 * This function is used to disconnect web socket connection
 * 
 */
function disconnectWS() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	/**
	 * call the disconnect reader function and pass disconnectCB to be executed
	 * after the response is received from server
	 */
	showLoader();
	readerClass.disconnect(disconnectCB);
}
/**
 * disconnect handler call back function.
 * This function is executed when response is received from server for disConnectReader request.
 * This function is passed as a disconnectCB call back function while sending disConnectReader request.
 * 
 * @param response describing response from server.
 *  response object has one field 'data' which contains a string in the json format.
 *  This string can be converted to json object by using JSON.parse(response.data);
 * 
 */
var disconnectCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		alert(error.message);
		return;
	}
	var result = response;
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	ToolkitOB.cleanup();
	if ('fail' === result) {
		return result.error + ' : ' + result.description;
	}
	changeButtonState(false);
}

/**
 * This function is to hide all the divs and only show a div
 * specified by divName
 * 
 * @param divName div to show/block
 */
function showDiv(divName) {
	var divs = document.getElementsByClassName('public-data-div');
	for (let i = 0; i <= divs.length - 1; i++) {
		divs[i].style.display = 'none';
	}
	if ('' !== divName) {
		document.getElementById(divName).style.display = 'block';
		document.getElementById(divName).style.display = 'block';
	}
}
/**
 * This function is to change button's accessibility and css class.
 * 
 */
function changeButtonState(flag) {
	if (flag == false) {
		if (self.IsNfc) {
			document.getElementById('disconnectBtn').disabled = false;
			document.getElementById('toolkitVersionBtn').disabled = false;
			document.getElementById('setNfcParams').disabled = false;
			document.getElementById('publicDataBtn').disabled = false;
			document.getElementById('verifyBioBtn').disabled = false;
			return;
		}
	}
	var buttons = document.getElementsByClassName("buttonInitial");
	for (let i = 0; i <= buttons.length - 1; i++) {
		buttons[i].disabled = flag;
	}
	if (self.IsNfc === false) {
		document.getElementById('setNfcParams').disabled = true;
	}
}
/**
 * This function is to display progress in progress box.
 */
function displayProgress(msg) {
	msg = msg + '\n' + document.getElementById("prgssText").value;
	document.getElementById("prgssText").value = msg;
}
/**
 * This function is to show unblock pin div
 */
function PinUnblock() {
	showDiv("unBlockPinDiv");
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	changeButtonState(true);
	readerClass.getFingerData(
		function (event, error) {
			if (error !== null) {
				alert(error.message);
				changeButtonState(true);
				return;
			}
			var result = event;
			if ('fail' === result.status) {
				return result.error + ' : ' + result.description;
			}
			/* set result of getFingerIndex to local variable so that it can be while verifying biometric */
			self.fingerData = result;
			var selectBox = document.getElementById("unBlockPinFingerSelect");
			if (selectBox.options.length > 1) {
				selectBox.removeChild(selectBox.options[2]);
				selectBox.removeChild(selectBox.options[1]);
			}
			var option1 = document.createElement("option");
			var opt1 = result[0].fingerIndex;
			option1.text = opt1;
			selectBox.add(option1);
			var option2 = document.createElement("option");
			option2.text = result[1].fingerIndex;
			selectBox.add(option2);
			changeButtonState(false);
		})
}

function UnBlockPinSubmit() {
	try {
		var pin = document.getElementById("unBlockPinTxtBx").value;
		if (undefined === pin || '' === pin || pin.length < 4) {
			alert('Please provide valid pin .');
			return;
		}
		var selectedFinger = document.getElementById("unBlockPinFingerSelect").value;
		if (undefined === selectedFinger || '' === selectedFinger) {
			alert('Please select a finger.');
			return;
		}
		displayProgress('Matching biometric ...');
		if (null === readerClass || undefined === readerClass) {
			alert('ERROR : Websocket is not initilaized.');
			return;
		}
		var index;
		for (let i = 0; i < self.fingerData.length; i++) {
			if (self.fingerData[i].fingerIndex === selectedFinger) {
				index = self.fingerData[i];
				break;
			}
		}
		showLoader();
		PrepareRequest(function (requestHandle) {
			if (requestHandle === undefined || requestHandle === null) {
				var encodedPin = pin;
				var sensor_timeout = 30; /*  seconds */
				readerClass.unblockPin(
					encodedPin,
					index,
					sensor_timeout,
					function (event, error) {
						hideLoader();
						if (error) {
							if (error.attemptsLeft) {
								error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
							}
							document.getElementById("unBlockPinBioTxtBx").style.color = "red";
							document.getElementById("unBlockPinBioTxtBx").value = error.message || "Failed try again later.";
							document.getElementById("unBlockPinBioTxtBx").type = "text";
							return;
						}
						var result = event;
						document.getElementById("unBlockPinBioTxtBx").style.color = "green";
						document.getElementById("unBlockPinBioTxtBx").value = "Successful.";
						document.getElementById("unBlockPinBioTxtBx").type = "text";
						self.fingerData = null;
						/* enable all buttons as request is processed */
						changeButtonState(false);
					});
			} else {
				ToolkitOB.getDataProtectionKey(
						function (response, error) {
				var encodedPin = encodePinOnServer(pin, requestHandle, response.publicKey);
				if (encodedPin == -1) {
					hideLoader();
					changeButtonState(false);
					alert('Failed to Encrypt data');
					return;
				}
				var sensor_timeout = 30; /*  seconds */
				readerClass.unblockPin(
					encodedPin,
					index,
					sensor_timeout,
					function (event, error) {
						hideLoader();
						if (error) {
							if (error.attemptsLeft) {
								error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
							}
							document.getElementById("unBlockPinBioTxtBx").style.color = "red";
							document.getElementById("unBlockPinBioTxtBx").value = error.message || "Failed try again later.";
							document.getElementById("unBlockPinBioTxtBx").type = "text";
							return;
						}
						var result = event;
						document.getElementById("unBlockPinBioTxtBx").style.color = "green";
						document.getElementById("unBlockPinBioTxtBx").value = "Successful.";
						document.getElementById("unBlockPinBioTxtBx").type = "text";
						self.fingerData = null;
						/* enable all buttons as request is processed */
						changeButtonState(false);
					});
						})

			}

		});
	} catch (e) {
		hideLoader();
		return "An exception occured in unblockPIN." + e;
	}
}

/**
 * This function is to show card genuine div
 */
function CardGenuine() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	showDiv("cardGenuineDiv");
	/*  disable all buttons till request is processe */
	changeButtonState(true);
	showLoader();
	var randomStr = generateRandomString(40);
	var requestId = btoa(randomStr);
	readerClass.isCardGenuine(requestId, CardGenuineCB);
}
var CardGenuineCB = function (response, error) {
	hideLoader();
	if (null !== error) {
		document.getElementById("cardGenuineTxtBx").style.color = "red";
		document.getElementById("cardGenuineTxtBx").value = "Failed. : " + error.message;
		document.getElementById("cardGenuineTxtBx").type = "text";
		return;
	}
	result = JSON.parse(event.data);
	/*  display success message */
	document.getElementById("cardGenuineTxtBx").style.color = "green";
	document.getElementById("cardGenuineTxtBx").value = "Successful.";
	document.getElementById("cardGenuineTxtBx").type = "text";
	/*  reset the fingerData to null */
	self.fingerData = null;
	/*  enable all buttons as request is processed */
	changeButtonState(false);
}

function SignXMLFunc() {
	showDiv("signXADESDiv");
}

function SignXMLSubmit() {
	try {
		var xmlFilePath = document.getElementById("signXADESinputTxtBx").value;
		var signedXmlFilePath = document.getElementById("signXADESouputTxtBx").value;
		signingContext.signatureLevel = document.getElementById("signatureLevelSelect").value;
		signingContext.packagingMode = document.getElementById("pkgModeSelect").value;
		signingContext.userPin = document.getElementById("pinTxtBx").value;
		signingContext.tsaUrl = document.getElementById("tsaUrlTxtBx").value;
		signingContext.ocspUrl = document.getElementById("ocspUrlTxtBx").value;
		signingContext.certPath = document.getElementById("certPathTxtBx").value;
		signingContext.countryCode = document.getElementById("countryTxtBx").value;
		signingContext.locality = document.getElementById("localityTxtBx").value;
		signingContext.postalCode = document.getElementById("postalCodeTxtBx").value;
		signingContext.stateOrProvince = document.getElementById("stateTxtBx").value;
		signingContext.street = document.getElementById("streetTxtBx").value;
		if (undefined === xmlFilePath || '' === xmlFilePath) {
			alert('Please provide valid file path .');
			return;
		}
		if (undefined === signingContext.userPin || '' === signingContext.userPin || signingContext.userPin < 4) {
			alert('Please provide pin .');
			return;
		}
		if (signingContext.packagingMode != 3) {
			if (undefined === signedXmlFilePath || '' === signedXmlFilePath) {
				alert('Please provide valid file path .');
				return;
			}
		}
		if (null === readerClass || undefined === readerClass) {
			alert('ERROR : Websocket is not initilaized.');
			return;
		}
		showLoader();
		PrepareRequest(function (requestHandle) {
			if (requestHandle === undefined || requestHandle === null) {
				var encodedPin = signingContext.userPin;
				displayProgress('Signing...');
				signingContext.userPin = encodedPin;
				readerClass.xadesSign(signingContext, xmlFilePath, signedXmlFilePath, SignXMLCB);
			} else {

				ToolkitOB.getDataProtectionKey(
						function (response, error) {
				var encodedPin = encodePinOnServer(signingContext.userPin, requestHandle, response.publicKey);
				if (encodedPin == -1) {
					hideLoader();
					changeButtonState(false);
					alert('Failed to Encrypt data');
					return;
				}
				displayProgress('Signing...');
				signingContext.userPin = encodedPin;
				readerClass.xadesSign(signingContext, xmlFilePath, signedXmlFilePath, SignXMLCB);
						})
			}

		});
	} catch (e) {
		hideLoader();
		return "An exception occured ." + e;
	}
}

var SignXMLCB = function (event, error) {
	hideLoader();
	if (error !== null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		document.getElementById("signXADESResultTxtBx").style.display = null;
		document.getElementById("signXADESResultTxtBx").style.color = "red";
		document.getElementById("signXADESResultTxtBx").innerHTML = error.message;
		return;
	}
	document.getElementById("signXADESResultTxtBx").style.color = "green";
	document.getElementById("signXADESResultTxtBx").innerHTML = event;
	document.getElementById("signXADESResultTxtBx").style.display = null;
	/* enable all buttons as request is processed */
	changeButtonState(false);
}

function VerifyXMLFunc() {
	showDiv("verifyXADESDiv");
}

function VerifyXMlSubmit() {
	try {
		var signedXmlFilePath = document.getElementById("verifyXADESTxtinputBx").value;
		verificationContext.ocspPath = document.getElementById("verifyXADESocspUrlTxtBx").value;
		verificationContext.certPath = document.getElementById("verifyXADEScertPathTxtBx").value;
		var signature = document.getElementById("verifyXADESSignedDtBx").value;
		verificationContext.packagingMode = document.getElementById("XVerifypkgModeSelect").value;
		verificationContext.report_type = document.getElementById("SVerifyReportSelect").value;
		if (undefined === verificationContext.report_type || '' === verificationContext.report_type) {
			alert('Please select report type.');
			return;
		}
		if (undefined === signedXmlFilePath || '' === signedXmlFilePath) {
			alert('Please provide valid file path .');
			return;
		}
		verificationContext.detachedValue = 0;
		if (verificationContext.packagingMode == 3) {
			verificationContext.detachedValue = 1;
			if (undefined === signature || '' === signature) {
				alert('Please provide valid Signed data .');
				return;
			}
		} else {
			signature = null;
		}
		if (null === readerClass || undefined === readerClass) {
			alert('ERROR : Websocket is not initilaized.');
			return;
		}
		displayProgress('Verifying...');
		showLoader();
		var ret = readerClass.xadesVerify(verificationContext, signedXmlFilePath, signature, VerifyXMlSubmitCB)
		return;
	} catch (e) {
		return "An exception occured when reading public data." + e;
	}
}

var VerifyXMlSubmitCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		alert(error.message);
		changeButtonState(true);
		document.getElementById("verifyXADESResultTxtBx").style.display = null;
		document.getElementById("verifyXADESResultTxtBx").value = error.message;
		return;
	}
	document.getElementById("verifyXADESResultTxtBx").style.display = null;
	document.getElementById("verifyXADESResultTxtBx").value = response;
	changeButtonState(false);
}

function SignPDFFunc() {
	showDiv("signPADESDiv");
}

function SignPDFSubmit() {
	try {
		var pdfFilePath = document.getElementById("signPADESinputTxtBx").value;
		var signedPdfFilePath = document.getElementById("signPADESouputTxtBx").value;
		signingContext.signatureLevel = document.getElementById("PsignatureLevelSelect").value;
		signingContext.packagingMode = document.getElementById("PpkgModeSelect").value;
		signingContext.userPin = document.getElementById("PpinTxtBx").value;
		signingContext.tsaUrl = document.getElementById("PtsaUrlTxtBx").value;
		signingContext.ocspUrl = document.getElementById("PocspUrlTxtBx").value;
		signingContext.certPath = document.getElementById("PcertPathTxtBx").value;
		signingContext.countryCode = document.getElementById("PcountryTxtBx").value;
		signingContext.locality = document.getElementById("PlocalityTxtBx").value;
		signingContext.postalCode = document.getElementById("PpostalCodeTxtBx").value;
		signingContext.stateOrProvince = document.getElementById("PstateTxtBx").value;
		signingContext.street = document.getElementById("PstreetTxtBx").value;
		signingContext.signNmPositionSelect = document.getElementById("signNmPositionSelect").value;
		signingContext.sigVisibleSelect = document.getElementById("sigVisibleSelect").value;
		signingContext.pgNumberTxtBx = document.getElementById("pgNumberTxtBx").value;
		signingContext.sigTextTxtBx = document.getElementById("sigTextTxtBx").value;
		signingContext.fontNameTxtBx = document.getElementById("fontNameTxtBx").value;
		signingContext.fontSizeTxtBx = document.getElementById("fontSizeTxtBx").value;
		signingContext.fontColorTxtBx = document.getElementById("fontColorTxtBx").value;
		signingContext.bgColorTxtBx = document.getElementById("bgColorTxtBx").value;
		signingContext.sigImgPathTxtBx = document.getElementById("sigImgPathTxtBx").value;
		signingContext.sigYaxisTxtBx = document.getElementById("sigYaxisTxtBx").value;
		signingContext.sigXaxisTxtBx = document.getElementById("sigXaxisTxtBx").value;
		signingContext.signerContactInfoTxtBx = document.getElementById("signerContactInfoTxtBx").value;
		signingContext.signerLocationTxtBx = document.getElementById("signerLocationTxtBx").value;
		signingContext.reasonSignTxtBx = document.getElementById("reasonSignTxtBx").value;
		if (undefined === pdfFilePath || '' === pdfFilePath) {
			alert('Please provide valid file path .');
			return;
		}
		if (undefined === signedPdfFilePath || '' === signedPdfFilePath) {
			alert('Please provide valid file path .');
			return;
		}
		if (undefined === signingContext.userPin || '' === signingContext.userPin || signingContext.userPin < 4) {
			alert('Please provide pin .');
			return;
		}
		/* check output file path only if packing mode is not detached */
		if (signingContext.packagingMode != 3) {}
		if (null === readerClass || undefined === readerClass) {
			alert('ERROR : Websocket is not initilaized.');
			return;
		}
		showLoader();
		PrepareRequest(function (requestHandle) {
			if (requestHandle === undefined || requestHandle === null) {
				var encodedPin = signingContext.userPin;
				displayProgress('Signing...');
				signingContext.userPin = encodedPin;
				readerClass.padesSign(signingContext, pdfFilePath, signedPdfFilePath, SignPDFSubmitCB);
			} else {
				ToolkitOB.getDataProtectionKey(
						function (response, error) {
				var encodedPin = encodePinOnServer(signingContext.userPin, requestHandle, response.publicKey);
				if (encodedPin == -1) {
					hideLoader();
					changeButtonState(false);
					alert('Failed to Encrypt data');
					return;
				}
				displayProgress('Signing...');
				signingContext.userPin = encodedPin;
				readerClass.padesSign(signingContext, pdfFilePath, signedPdfFilePath, SignPDFSubmitCB);
						})
			}

		});
	} catch (e) {
		hideLoader();
		alert(e.message);
	}
}
var SignPDFSubmitCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		document.getElementById("signPADESResultTxtBx").style.display = null;
		document.getElementById("signPADESResultTxtBx").innerHTML = error.message;
		return;
	}
	document.getElementById("signPADESResultTxtBx").style.display = null;
	document.getElementById("signPADESResultTxtBx").innerHTML = response;
	/* enable all buttons as request is processed */
	changeButtonState(false);
}

function VerifyPDFFunc() {
	showDiv("verifyPADESDiv");
}

function VerifyPDFSubmit() {
	try {
		var signedPdfFilePath = document.getElementById("verifyPADESTxtinputBx").value;
		verificationContext.ocspPath = document.getElementById("verifyPADESocspUrlTxtBx").value;
		verificationContext.certPath = document.getElementById("verifyPADEScertPathTxtBx").value;
		verificationContext.packagingMode = document.getElementById("PVerifypkgModeSelect").value;
		verificationContext.report_type = document.getElementById("PVerifyReportSelect").value;
		if (undefined === verificationContext.report_type || '' === verificationContext.report_type) {
			alert('Please select report type.');
			return;
		}
		if (undefined === signedPdfFilePath || '' === signedPdfFilePath) {
			alert('Please provide valid file path .');
			return;
		}
		verificationContext.detachedValue = 0;
		if (verificationContext.packaging_mode == 3) {
			verificationContext.detachedValue = 1;
			if (undefined === verificationContext.signedData || '' === verificationContext.signedData) {
				alert('Please provide valid Signed data .');
				return;
			}
		}
		if (null === readerClass || undefined === readerClass) {
			alert('ERROR : Websocket is not initilaized.');
			return;
		}
		displayProgress('Verifying...');
		showLoader();
		readerClass.padesVerify(verificationContext, signedPdfFilePath, VerifyPDFCB);
	} catch (e) {
		hideLoader();
		alert(e.message);
		return "An exception occured when reading public data." + e;
	}
}
var VerifyPDFCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		alert(error.message);
		document.getElementById("verifyPADESResultTxtBx").style.display = null;
		document.getElementById("verifyPADESResultTxtBx").style.color = "red";
		document.getElementById("verifyPADESResultTxtBx").value = error.message;
		return;
	}
	document.getElementById("verifyPADESResultTxtBx").style.color = "green";
	document.getElementById("verifyPADESResultTxtBx").style.display = null;
	document.getElementById("verifyPADESResultTxtBx").value = response;
	changeButtonState(false);

}

function SignOtrFile() {
	showDiv("signCADESDiv");
}

function SignOtrFileSubmit() {
	try {
		var inputFilePath = document.getElementById("signCADESinputTxtBx").value;
		signingContext.signatureLevel = document.getElementById("CsignatureLevelSelect").value;
		signingContext.packagingMode = document.getElementById("CSignpkgModeSelect").value;
		signingContext.userPin = document.getElementById("CpinTxtBx").value;
		signingContext.tsaUrl = document.getElementById("CtsaUrlTxtBx").value;
		signingContext.ocspUrl = document.getElementById("CocspUrlTxtBx").value;
		signingContext.certPath = document.getElementById("CcertPathTxtBx").value;
		signingContext.countryCode = document.getElementById("CcountryTxtBx").value;
		signingContext.locality = document.getElementById("ClocalityTxtBx").value;
		signingContext.postalCode = document.getElementById("CpostalCodeTxtBx").value;
		signingContext.stateOrProvince = document.getElementById("CstateTxtBx").value;
		signingContext.street = document.getElementById("CstreetTxtBx").value;
		if (undefined === inputFilePath || '' === inputFilePath) {
			alert('Please provide valid file path .');
			return;
		}
		if (null === readerClass || undefined === readerClass) {
			alert('ERROR : Websocket is not initilaized.');
			return;
		}
		if (undefined === signingContext.userPin || '' === signingContext.userPin || signingContext.userPin < 4) {
			alert('Please provide pin .');
			return;
		}
		signingContext.packagingMode = 3;
		showLoader();
		PrepareRequest(function (requestHandle) {
			if (requestHandle === undefined || requestHandle === null) {
				var encodedPin = signingContext.userPin;
				displayProgress('Signing...');
				signingContext.userPin = encodedPin;
				readerClass.cadesSign(signingContext, inputFilePath, SignOtrFileCB);
			} else {
				ToolkitOB.getDataProtectionKey(
						function (response, error) {
				var encodedPin = encodePinOnServer(signingContext.userPin, requestHandle, response.publicKey);
				if (encodedPin == -1) {
					hideLoader();
					changeButtonState(false);
					alert('Failed to Encrypt data');
					return;
				}
				displayProgress('Signing...');
				signingContext.userPin = encodedPin;
				readerClass.cadesSign(signingContext, inputFilePath, SignOtrFileCB);
						})
			}

		});
	} catch (e) {
		hideLoader();
		alert(e.message);
		return "An exception occured when reading public data." + e;
	}

}
var SignOtrFileCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		alert(error.message);
		document.getElementById("signCADESResultTxtBx").style.display = null;
		document.getElementById("signCADESResultTxtBx").innerHTML = error.message;
		return;
	}
	document.getElementById("signCADESResultTxtBx").style.display = null;
	document.getElementById("signCADESResultTxtBx").value = response;
	/* enable all buttons as request is processed */
	changeButtonState(false);
}

function VerifyOtrFile() {
	showDiv("verifyCADESDiv");
}

function VerifyOtrFileSubmit() {
	try {
		var inputFilePath = document.getElementById("verifyCADESTxtinputBx").value;
		verificationContext.ocspPath = document.getElementById("verifyCADESocspUrlTxtBx").value;
		verificationContext.certPath = document.getElementById("verifyCADEScertPathTxtBx").value;
		var signature = document.getElementById("verifyCADESSignDataPathTxtBx").value;
		verificationContext.report_type = document.getElementById("CVerifyReportSelect").value;
		if (undefined === verificationContext.report_type || '' === verificationContext.report_type) {
			alert('Please select report type.');
			return;
		}
		if (undefined === inputFilePath || '' === inputFilePath) {
			alert('Please provide valid file path .');
			return;
		}
		if (undefined === signature || '' === signature) {
			alert('Please provide valid valid signed data .');
			return;
		}

		if (null === readerClass || undefined === readerClass) {
			alert('ERROR : Websocket is not initilaized.');
			return;
		}
		verificationContext.detached = 1;
		displayProgress('Verifying...');
		showLoader();
		readerClass.cadesVerify(verificationContext, inputFilePath, signature, VerifyOtrFileCB);
	} catch (e) {
		return "An exception occured when reading public data." + e;
	}

}
var VerifyOtrFileCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		alert(error.message);
		document.getElementById("verifyCADESResultTxtBx").style.display = null;
		document.getElementById("verifyCADESResultTxtBx").style.color = "red";
		document.getElementById("verifyCADESResultTxtBx").value = error.message;
		return;
	}
	document.getElementById("verifyCADESResultTxtBx").style.color = "green";
	document.getElementById("verifyCADESResultTxtBx").style.display = null;
	document.getElementById("verifyCADESResultTxtBx").value = response;
	/* enable all buttons as request is processed */
	changeButtonState(false);
}

function DisplayFamilyBookDataN() {
	showDiv("familyBookDiv");

}

function familyBkDtPinSubmit() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Reader is not initiaized.');
		return;
	}
	var pin = document.getElementById("familyBkDtPinTxtBx").value;
	if (undefined === pin || '' === pin || pin < 4) {
		alert('Please provide valid pin .');
		return;
	}
	displayProgress('Reading Family Book Data...');
	showLoader();
	PrepareRequest(function (requestHandle) {
		if (requestHandle === undefined || requestHandle === null) {
			var encodedPin = pin;
			displayProgress('fetching family book data');
			readerClass.readFamilyBookData(encodedPin, familyBkDtPinSubmitCB);
		} else {
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
			var encodedPin = encodePinOnServer(pin, requestHandle, response.publicKey);
			if (encodedPin == -1) {
				hideLoader();
				changeButtonState(false);
				alert('Failed to Encrypt data');
				return;
			}
			displayProgress('fetching family book data');
			readerClass.readFamilyBookData(encodedPin, familyBkDtPinSubmitCB);
					})
		}
		/* call readFamilyBook	 */

	});
}
var familyBkDtPinSubmitCB = function (response, error) {
	if (response === null) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		hideLoader();
		alert(error.message);
		return;
	}
	document.getElementById('dispalyfmaily').style.display = null;
	if (null != error) {
		alert(error.message);
		return;
	}
	dataBindDom(response.HeadOfFamily, 'headOfFamData');
	for (let i = 0; i < response.wives.length; i++) {
		dataBindDom(response.wives[i], 'famWifeData');
	}
	for (let i = 0; i < response.children.length; i++) {
		dataBindDom(response.children[i], 'famChildData');
	}
	setVerifyXml('familyBookDiv', response.xmlString);
	/* enable all buttons as request is processed */
	changeButtonState(false);
	hideLoader();
}

var prepareRequestCB = function (response, error) {
	var userName = prompt('enter username', '');
	var password = prompt('password', '');
	var deviceRefID = prompt('Device Reference Id', '')
	if (null == error) {
		var requestHandle = response;
		if (requestHandle == null || requestHandle == undefined) {
			var userNameEncoded = userName;
			var passwordEncoded = password;
			ToolkitOB.registerDevice(userNameEncoded, passwordEncoded, deviceRefID, registerDeviceCB);
		} else {
			
			ToolkitOB.getDataProtectionKey(
					function (response, error) {
						var userNameEncoded = encryptParamasOnServer(userName, requestHandle, response.publicKey);
						var passwordEncoded = encryptParamasOnServer(password, requestHandle, response.publicKey);
						ToolkitOB.registerDevice(userNameEncoded, passwordEncoded, deviceRefID, registerDeviceCB);
						
					}// cb
			
			)// getDataProtectionKey()
		}
	} else {
		hideLoader();
		alert(error.message);
	}
}

function pkgModeSelectChange() {
	document.getElementById("signedFileDivXADES").style.display = 'block';
	var packaging_mode = document.getElementById("pkgModeSelect").value;
	if (packaging_mode == 3) {
		document.getElementById("signedFileDivXADES").style.display = 'none';
	}
}

function XVerifypkgModeSelectChange() {
	var packaging_mode = document.getElementById("XVerifypkgModeSelect").value;
	document.getElementById("XverifyInSignedDtDiv").style.display = 'none';
	if (packaging_mode == 3) {
		document.getElementById("XverifyInSignedDtDiv").style.display = 'block';
		document.getElementById("verifyXADESLbl").innerHTML = 'Enter original file path :';
	} else {
		document.getElementById("verifyXADESLbl").innerHTML = 'Enter XML Signed File Path <br> (with file name and extension) : &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp';
	}
}

function ToolkitVersion() {
	if (null === readerClass || undefined === readerClass) {
		alert("The Webcomponent is not initialized.");
		return;
	}
	showLoader();
	ToolkitOB.getToolkitVersion(ToolkitVersionCB);
}
var ToolkitVersionCB = function (response, error) {
	hideLoader();
	if (error !== null) {
		alert(error.message);
		return;
	}
	var result = response;
	if ('Fail' === result.status) {
		alert("Error while getting Toolkit Version :" + result.error + ' : ' + result.description);
	}
	// var str = result.etc_major + "." +
	// 	result.etc_minor + "." +
	// 	result.etc_patch;
	alert("Toolkit Version :" + result);
}

function generateRandomString(length) {
	var text = "";
	var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
	for (var i = 0; i < length; i++) {
		text += possible.charAt(Math.floor(Math.random() * possible.length));
	}
	return text;
}
var registerDeviceCB = function (response, error) {
	hideLoader();
	if (null != response) {
		alert("Registration Successfull Your ID is:" + response.deviceRegistrationID);
	} else {
		alert(error.description || error.message);
	}
}

function registerDevice() {
	/* generate the random string */
	var randomStr = generateRandomString(40);
	/*  convert randomString to base64 */
	var requestId = btoa(randomStr);
	showLoader();
	ToolkitOB.prepareRequest(requestId, prepareRequestCB);
}

function NFCAuth() {
	showDiv("setNfcParamsDiv");
}

function NFCAuthSubmit() {
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Reader is not initiaized.');
		return;
	}
	changeButtonState(true);
	var radios = document.getElementsByName('nfcgroup');
	for (var i = 0, length = radios.length; i < length; i++) {
		if (radios[i].checked) {
			if (radios[i].value === 'cdetails') {
				var cardnumber = document.getElementById('NfcParamscardNum').value;
				if (cardnumber == null || cardnumber == undefined) {
					alert("enter card number");
				}
				var dob = document.getElementById('NfcParamsDOB').value;
				if (dob == null || dob == undefined) {
					alert("enter valid date");
				}
				var expirydate = document.getElementById('NfcParamsED').value;
				if (expirydate == null || expirydate == undefined) {
					alert("enter valid date");
				}
				expirydate = getEidaDate(expirydate);
				dob = getEidaDate(dob);
				showLoader();
				readerClass.setNfcAuthenticationParameters(cardnumber, dob, expirydate, NFCAuthCB);
			} else {
				showLoader();
				var mzrData = document.getElementById('NfcParamsmrzData').value;
				readerClass.setNfcAuthenticationParameters(mzrData, NFCAuthCB);
			}
			break;
		}
	}
}
var NFCAuthCB = function (response, error) {
	hideLoader();
	if (error) {
		changeButtonState(false);
		alert(error.message);
		self.IsNfc = true;
		return;
	}
	self.IsNfc = true;
	var message = document.getElementById('NfcParamsmrzData');
	message.style.color = "green";
	message.value = "Success";

	document.getElementById('publicDataBtn').disabled = false;
	document.getElementById('verifyBioBtn').disabled = false;
	document.getElementById('disconnectBtn').disabled = false;
	document.getElementById('toolkitVersionBtn').disabled = false;
}

function deviceId() {
	if (null === ToolkitOB || undefined === ToolkitOB) {
		alert('ERROR : Application is not initiaized.');
		return;
	}
	changeButtonState(true);
	ToolkitOB.getDeviceId(deviceIdCB);
}
var deviceIdCB = function (response, error) {
	if (error) {
		changeButtonState(false);
		alert(error.message);
		// self.IsNfc = true;
		return;
	}
	changeButtonState(false);
	alert(response);
}

function MatchOnCard() {
	showDiv("matchOncardDiv");
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	changeButtonState(true);
	readerClass.getFingerData(
		function (event, error) {
			if (error !== null) {
				alert(error.message);
				changeButtonState(true);
				return;
			}
			var result = event;
			if ('fail' === result.status) {
				return result.error + ' : ' + result.description;
			}
			/* set result of getFingerIndex to local variable so that it can be while verifying biometric */
			self.fingerData = result;
			var selectBox = document.getElementById("matchonPinFingerSelect");
			if (selectBox.options.length > 1) {
				selectBox.removeChild(selectBox.options[2]);
				selectBox.removeChild(selectBox.options[1]);
			}
			var option1 = document.createElement("option");
			var opt1 = result[0].fingerIndex;
			option1.text = opt1;
			selectBox.add(option1);
			var option2 = document.createElement("option");
			option2.text = result[1].fingerIndex;
			selectBox.add(option2);
			changeButtonState(false);
		}
	);
}

function MatchOnCardSubmit() {
	var selectedFinger = document.getElementById("matchonPinFingerSelect").value;
	if (undefined === selectedFinger || '' === selectedFinger) {
		alert('Please select a finger.');
		return;
	}
	displayProgress('Matching biometric ...');
	if (null === readerClass || undefined === readerClass) {
		alert('ERROR : Websocket is not initilaized.');
		return;
	}
	var index;
	for (let i = 0; i < self.fingerData.length; i++) {
		if (self.fingerData[i].fingerIndex === selectedFinger) {
			index = self.fingerData[i];
			break;
		}
	}
	showLoader();
	var randomStr = generateRandomString(40);
	var requestId = btoa(randomStr);
	var sensor_timeout = 30; /*  seconds */
	readerClass.matchOnCard(requestId, index, sensor_timeout, MatchOnCardCB);
}
var MatchOnCardCB = function (response, error) {
	hideLoader();
	if (error) {
		if (error.attemptsLeft) {
			error.message = error.message + "   " + "Attemptsleft:" + error.attemptsLeft;
		}
		document.getElementById("matchonPinBioTxtBx").style.color = "red";
		document.getElementById("matchonPinBioTxtBx").value = error.message || "Failed try again later.";
		document.getElementById("matchonPinBioTxtBx").type = "text";
		return;
	}
	var result = event;
	document.getElementById("matchonPinBioTxtBx").style.color = "green";
	document.getElementById("matchonPinBioTxtBx").value = "Successful.";
	document.getElementById("matchonPinBioTxtBx").type = "text";
	self.fingerData = null;
	/* enable all buttons as request is processed */
	changeButtonState(false);
}

function setVerifyXml(id, xml) {
	removeTable();
	var div = document.getElementById(id);
	var tbl = document.createElement("table");
	tbl.setAttribute("id", "verifyxmltbl");
	tbl.setAttribute("align", "center");
	var row = document.createElement("tr");
	var cell = document.createElement("td");
	var span = document.createElement('span');
	var row1 = document.createElement("tr");
	var cell1 = document.createElement("td");
	var span1 = document.createElement('span');
	span1.setAttribute('id', "verifyxmlmsg");
	span.innerHTML = '<button class="button"  onclick="verifyXML()"><span>Verify XML Signature</span></button>';
	self.verifyxmldata = xml;
	cell.appendChild(span);
	row.appendChild(cell);
	tbl.appendChild(row);
	cell1.appendChild(span1);
	row1.appendChild(cell1);
	tbl.appendChild(row1);
	div.appendChild(tbl);
}

function removeTable() {
	var tbl = document.getElementById('verifyxmltbl');
	if (tbl) tbl.parentNode.removeChild(tbl);
}

function verifyXML(xml) {
	var ValidateXML = verifyXMlOnServer(self.verifyxmldata);
	var msgbox = document.getElementById('verifyxmlmsg');
	msgbox.innerHTML = ValidateXML.message;
	if (ValidateXML.status === 'SUCCESS') {
		msgbox.style.color = "green";
	} else {
		msgbox.style.color = "red";
	}
}

function getEidaDate(value) {
	var dates = value.split('-');
	dates[0] = dates[0].slice(-2);
	value = dates.join("");
	return value;
}

function PrepareRequest(callback) {
	var randomStr = generateRandomString(40);
	var requestId = btoa(randomStr);
	readerClass.prepareRequest(requestId,
		function (response, error) {
			if (null == error) {
				var requestHandle = response;
				callback(requestHandle);
			} else {
				console.log(error);
				hideLoader();
				alert(error.message);
				// throw error
			}
		}
	);
}
/**
 * This function is used to display public data on page
 */
function displayData(response, div) {
	document.getElementById('IDNumber_data').innerHTML = response.iDNumber;
	document.getElementById('CardNumber_data').innerHTML = response.cardNumber;
	document.getElementById('Cardsl_no').innerHTML = response.cardSerialNumber;
	document.getElementById('Request_id').innerHTML = response.requestId;
	document.getElementById('service').innerHTML = response.service;
	document.getElementById('pubphoto').src = "data:image/bmp;base64," + response.cardHolderPhoto;
	dataBindDom(response.modifiablePublicData, 'md-DataTable');
	dataBindDom(response.nonModifiablePublicData, 'nmd-DataTable');
	if (!self.IsNfc) {
		dataBindDom(response.homeAddress, 'hm_address_data');
		dataBindDom(response.workAddress, 'wrk_address_data');
	} else {
		var address1 = document.getElementById('hm_address_data');
		var address2 = document.getElementById('wrk_address_data');
		address1.style.display = 'none';
		address2.style.display = 'none';
		address1.style.display = null;
		address2.style.display = null;
	}
	showDiv("poublicDataDiv");
}

function dataBindDom(response, id) {
	var div = document.getElementById(id);
	for (let key in response) {
		let tr = document.createElement('tr');
		let td1 = document.createElement('td');
		let lbl1 = document.createElement('label');
		lbl1.innerHTML = key + '  :';
		td1.appendChild(lbl1);
		tr.appendChild(td1);
		let td2 = document.createElement('td');
		let lbl2 = document.createElement('label');
		lbl2.innerHTML = Object.keys(response[key] ? response[key] : '').length === 0 ? '---' : response[key];
		td2.appendChild(lbl2);
		tr.appendChild(td2);
		div.appendChild(tr);
	}
}
Object.keys = function (obj) {
	var keys = [];

	for (var i in obj) {
		if (obj.hasOwnProperty(i)) {
			keys.push(i);
		}
	}

	return keys;
};

function hidetable(val) {
	document.getElementById(val).style.display = 'none';
}

function showtable(val) {
	document.getElementById(val).style.display = null;
}

function encodePinOnServer(pin, requestHandle, publicKey) {
	try {
		var xhttp = new XMLHttpRequest();
		xhttp.open("POST", javaService + "ToolkitController/pki/encode", false);
		xhttp.setRequestHeader("Content-type", "application/json");
		var ENCODE_PIN = {
			pin: pin,
			requestHandle: requestHandle,
			publicKey: publicKey
		};
		var request = JSON.stringify(ENCODE_PIN);
		xhttp.send(request);
		var response = xhttp.responseText;
		response = JSON.parse(response);
		
		return response.message;
	} catch (e) {
		console.log('error =' + e);
	}
	return "-1";
}

function verifyXMlOnServer(xml) {
	try {
		var xhttp = new XMLHttpRequest();;
		xhttp.open("POST", javaService + "ToolkitController/pki/verify", false);
		xhttp.setRequestHeader("Content-type", "application/json");
		var ENCODE_PIN = {
			strXML: xml,
		};
		var request = JSON.stringify(ENCODE_PIN);
		xhttp.send(request);
		var response = xhttp.responseText;
		response = JSON.parse(response);
		return response;
	} catch (e) {
		console.log('error =' + e);
	}
	return "-1";
}

function encryptParamasOnServer(data, requestHandle, publicKey) {
	try {
		var xhttp = new XMLHttpRequest();
		xhttp.open("POST", javaService + "ToolkitController/pki/encrypt", false);
		xhttp.setRequestHeader("Content-type", "application/json");
		var ENCRYPT_DATA = {
			userName: data,
			requestHandle: requestHandle,
			publicKey: publicKey
		};
		var request = JSON.stringify(ENCRYPT_DATA);
		xhttp.send(request);
		var response = xhttp.responseText;
		response = JSON.parse(response);
		return response.message;
	} catch (e) {
		console.log('error =' + e);
	}
	return "-1";
}

function showLoader() {
	var cols = document.getElementsByClassName('custom-container-fluid');
	for (let i = 0; i < cols.length; i++) {
		cols[i].style.display = null;
	}
}

function hideLoader() {
	var cols = document.getElementsByClassName('custom-container-fluid');
	for (let i = 0; i < cols.length; i++) {
		cols[i].style.display = 'none';
	}
}