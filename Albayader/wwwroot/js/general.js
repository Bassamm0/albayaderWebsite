﻿// JavaScript Document

$(document).ready(function () {



	$('#processHolder').css('height', $(document).height());

	wHeight = screen.height;
	$('#PageManager').css('height', wHeight - 190)
	$('#managerContent').css('height', wHeight - 230)

	$(window).resize(function () {
		wHeight = screen.height;
		$('#PageManager').css('height', wHeight - 190)
		$('#managerContent').css('height', wHeight - 230)
	});

	$('[data-toggle="tooltip"]').tooltip();

	// OVERWRITES old selecor
	jQuery.expr[':'].contains = function (a, i, m) {
		return jQuery(a).text().toUpperCase()
			.indexOf(m[3].toUpperCase()) >= 0;
	};


	var populate = function (text, val, controlId) {
		$(controlId).append('<option value=' + val + '>' + text + '</option>');

	}

	// Whack fullscreen
	function cancelFullscreen() {
		if (document.cancelFullScreen) {
			document.cancelFullScreen();
		} else if (document.mozCancelFullScreen) {
			document.mozCancelFullScreen();
		} else if (document.webkitCancelFullScreen) {
			document.webkitCancelFullScreen();
		}
	}

	formatJSONDate = function (dateAsFromServerSide) {
		if (dateAsFromServerSide == '/Date(-62135596800000)/')
			return;
		var currentTime = new Date(parseInt(dateAsFromServerSide.substr(6)));
		var month = currentTime.getMonth() + 1;
		var day = currentTime.getDate();
		var year = currentTime.getFullYear();
		var date = addZero(month) + "-" + addZero(day) + "-" + addZero(year);
		return date;
	}
	formatJSONDateTime = function (dateAsFromServerSide) {
		if (dateAsFromServerSide == '/Date(-62135596800000)/')
			return 'Not Started';
		var currentTime = new Date(parseInt(dateAsFromServerSide.substr(6)));
		var month = currentTime.getMonth() + 1;
		var day = currentTime.getDate();
		var year = currentTime.getFullYear();
		var hours = currentTime.getHours();
		var minute = currentTime.getMinutes();
		var second = currentTime.getSeconds();
		var date = addZero(month) + "-" + addZero(day) + "-" + year + " " + addZero(hours) + ":" + addZero(minute) + ":" + second;;
		return date;
	}
	// check if empty
	isEmpty = function (str) {
		return (!str || 0 === str.length);
	}

	formatJSONDateTimeAMPM = function (dateAsFromServerSide) {
		if (dateAsFromServerSide == '/Date(-62135596800000)/')
			return 'Not Started';
		var currentTime = new Date(parseInt(dateAsFromServerSide.substr(6)));
		var month = currentTime.getMonth() + 1;
		var day = currentTime.getDate();
		var year = currentTime.getFullYear();
		var hours = currentTime.getHours();

		var amPM = (hours > 11) ? "pm" : "am";
		if (hours > 12) {
			hours -= 12;
		} else if (hours == 0) {
			hours = "12";
		}

		var minute = currentTime.getMinutes();
		var second = currentTime.getSeconds();
		var date = addZero(month) + "-" + addZero(day) + "-" + year + " " + addZero(hours) + ":" + addZero(minute) + ":" + second + " " + amPM;

		return date;
	}

	function addZero(str) {


		if (str.toString().length == 1) {
			str = '0' + str
		}

		return str;
	}
	// get file name
	fileNameFromUrl = function (url) {
		var index = url.lastIndexOf("/") + 1;
		var filename = url.substr(index);
		return filename

	}

	ASPDate = function (datatine) {
		
		var currentTime = new Date(datatine);
		var month = currentTime.getMonth() + 1;
		var day = currentTime.getDate();
		var year = currentTime.getFullYear();
		var date = addZero(month) + "-" + addZero(day) + "-" + addZero(year);
		return date;
	}

	RestoreOriginalFileName = function (sFileName) {
		if (!sFileName || 0 === sFileName.length) {
			return '';

		} else {

			var sOrgName = '';

			if (sFileName.indexOf("_") > -1) {

				sOrgName = sFileName.substring(0, sFileName.lastIndexOf('_'));
				sOrgName += sFileName.substring(sFileName.lastIndexOf('.'));

			}
		}

		return sOrgName;
	}




	resetForm = function (formid) {

		form = $('#' + formid);

		element = ['input', 'select', 'textarea'];
		for (i = 0; i < element.length; i++) {
			$.each(form.find(element[i]), function () {
				switch ($(this).attr('type')) {
					case 'text':
					case 'select-one':
					case 'textarea':
					case 'hidden':
					case 'file':
						$(this).val('');
						break;
					case 'checkbox':
					case 'radio':
						$(this).attr('checked', false);
						break;
					case 'select':
						alert($(this).val())
						$(this).attr('selected', false);


						break;
				}
			});
		}

		$(form).each(function () {
			$('textarea', this).val('');
		})
		$(form).each(function () {
			$('select', this).val('');
		})

		$('.fileinput-remove').click();
		$('.fileNameHolderCss').attr("href", '');
		$('.fileNameHolderCss').html('');
		$('.btnDeleteFileCss').css('display', 'none')
		ResetErrorValidation();
	}
	ResetErrorValidation = function () {

		$("label.error").hide();
		$(".error").removeClass("error");
	}

	w = $(window).width();
	$('#SuccessMessage,#ErrorMessage').css('left', w / 2 - 250)

	showSuccessMessage = function (msg) {
		height = $('#SuccessMessageHoder').outerHeight()
		h = screen.height / 2;
		_top = h - height;
		$('#SuccessMessage').animate({
			top: _top,
		}, 500);
		$("#SuccessMessage P").html(msg)
		setTimeout(function () {

			$('#SuccessMessage').animate({
				top: "-150px",
			}, 500);
			$("#SuccessMessage P").html('')
		}, 3000)
	}

	showErrorMessage = function (msg) {
		height = $('#SKErrorMessageHoder').outerHeight()
		h = screen.height / 2;
		_top = h - height;
		$('#ErrorMessage').animate({
			top: _top,
		}, 500);
		$("#ErrorMessage P").html(msg)
	}



	$('#ErrorMessage .fa-remove').click(function () {
		$('#ErrorMessage').animate({
			top: "-150px",
		}, 500);
		$("#ErrorMessage P").html('')
	});

	$('#SuccessMessage .fa-remove').click(function () {
		$('#SuccessMessage').animate({
			top: "-150px",
		}, 500);
		$("#SuccessMessage P").html('')
	});



	$('body').on('click', '#managerCloseBtn', function () {
		hideManger()
	});

	$('body').on('click', '.btnEdit', function () {
		showManger()
	});
	$('body').on('click', '.btnView', function () {
		showManger()
	});
	showManger = function () {
		$('#PageManager').css("display", "block");
		$('#PageManager').css("right", "0px");
		$('.PageContentCss').css("width", "70%");
		$('#managerContent').scrollTop(0);

	}
	hideManger = function () {
		$('#PageManager').css("display", "none");
		$('#PageManager').css("right", "-500px");
		$('.PageContentCss').css("width", "98.5%");
	}
	$('#AddNew,#btnEdit,#btnView').click(function () {
		ScrollManagerTop();
	});
	ScrollManagerTop = function () {

		$('#managerContent').animate({
			scrollTop: 0
		}, 'slow');
	}

	function ScrollDivTop(controlId) {
		$('#' + controlId).animate({ scrollTop: 0 }, 'slow');
	}


	// valudation methods for date  and others 
	// date
	$.validator.addMethod(
		"australianDate",//format mm/dd/yyyy.
		function (value, element) {
			if (value == '')
				return true;
			// put your own logic here, this is just a (crappy) example
			//  return value.match(/^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/);//format dd/mm/yyyy.
			return value.match(/^((0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?[0-9]{2})*$/);
		},
		"Please enter a date in the format mm/dd/yyyy."
	);
	//$.validator.addMethod(
	//       "australianDateAndNotRequired",
	//               function (value, element) {
	//                   if (value == '')
	//                       return true;
	//                   // put your own logic here, this is just a (crappy) example
	//                   return value.match(/^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/);
	//               },
	//           "Please enter a date in the format dd/mm/yyyy."
	//       );
	//decimal
	$.validator.addMethod(
		"decimal",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example(10,4)
			if (value.match(/^[0-9]{0,6}(\.[0-9]{1,7})?$/)) {
				return value.match(/^[0-9]{0,6}(\.[0-9]{1,7})?$/);
			} else if (value.match(/^-[0-9]{0,6}(\.[0-9]{1,7})?$/)) {
				return value.match(/^-[0-9]{0,6}(\.[0-9]{1,7})?$/);
			} else {
				return false;
			}

			//return value.match(/^-[0-9]{0,6}(\.[0-9]{1,7})?$/);
		},
		"Please enter correct value 1 to 6 ,1 to 4 example 123456.1234"
	);
	//tinyint 
	$.validator.addMethod(
		"tinyint",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example(10,4)
			if (value != '') {
				return value.match(/^(([0-1]?[0-9]?[0-9])|([2][0-4][0-9])|(25[0-5]))$/);
			} else {
				return true;
			}

		},
		"Please enter correct value from 0 -255"
	);
	// Smallint 
	$.validator.addMethod(
		"smallint",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example
			if (value > -32768 && value < 32767 || value == '')
				return true;
			else
				return false
		},
		"Please enter smallint value."
	);
	// int 
	$.validator.addMethod(
		"int",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example
			if (value > -9223372036854775808 && value < 9223372036854775807 || value == '')
				return true;
			else
				return false
		},
		"Please enter  int value."
	);
	// bit 
	$.validator.addMethod(
		"bit",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example
			if (value == '0' || value == 1 || value == '')
				return true;
			else
				return false
		},
		"Please enter 0 or 1."
	);
	// file images 
	$.validator.addMethod(
		"images",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example
			if (value.match(/\.(jpg)|(gif)|(png)|(bmp)|(eps)|(IFF)|(tga)|(tif)|(pdf)$/) || value == '')
				return true;
			else
				return false
		},
		"Sorry only images are allowed ."
	);
	// file rH 
	$.validator.addMethod(
		"rh",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example
			if (value.match(/\.(rh)$/) || value == '')
				return true;
			else
				return false
		},
		"Sorry only rh file are allowed ."
	);
	// file rH 
	$.validator.addMethod(
		"imageAndDoc",
		function (value, element) {
			// put your own logic here, this is just a (crappy) example
			if (value.match(/\.(jpg)|(gif)|(png)|(bmp)|(eps)|(IFF)|(tga)|(tif)|(pdf)|(txt)|(doc)|(xsl)|(docx)|(xslx)$/) || value == '')
				return true;
			else
				return false
		},
		"Sorry only images and (pdf),(txt),(doc),(xsl),(docx),(xslx) file are allowed ."
	);


	//// append copy right
	//var copyRight = ' <div class="CopyRight">';
	//copyRight += '<span style="line-height:30px;">';
	//copyRight += '&copy;     2015 Think Plus. All Rights Reserved';
	//copyRight += '</span>';
	//copyRight += '</div>';
	//$('body').append(copyRight)

});
