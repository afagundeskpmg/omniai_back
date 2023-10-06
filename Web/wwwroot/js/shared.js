function FormatarDecimal(numero) {
    if (numero != null && numero != '') {
        var numero = numero.toFixed(2).split('.');
        numero[0] = numero[0].split(/(?=(?:...)*$)/).join('.');
        return numero.join(',');
    }
    else
        return '';
}

function FormatarCNPJ(v) {
    if (v == null || v == '')
        v = '';
    else {
        v = v.replace(/[^0-9]/g, '');
        v = v.replace(/\D/g, '')
            .replace(/^(\d{2})(\d{3})?(\d{3})?(\d{4})?(\d{2})?/, "$1.$2.$3/$4-$5");

        if (v.length > 18)
            v = v.substring(0, 18);
    }
    return v;
}

function FormatarCPF(v) {
    if (v == null || v == '')
        v = '';
    else {
        if (v.length == 6)
            v = "***" + v + "**"

        v = v.replace(/(\D{3}|\d{3})(\d{3})(\d{3})(\D{2}|\d{2})/, "$1.$2.$3-$4")

        if (v.length > 14)
            v = v.substring(0, 14);
    }
    return v;
}

function FormatarDocumento(doc) {
    if (doc.length > 11)
        return FormatarCNPJ(doc)
    else
        return FormatarCPF(doc)
}

function ShowLoading() {
    $.blockUI({
        message: '<img src="../../images/gifs/loading.gif" width="100" />',
        overlayCSS: {
            backgroundColor: "#000",
            opacity: 0.5,
            cursor: "wait",
            "z-index": 2000,
        },
        css: {
            border: 0,
            padding: 0,
            color: "#333",
            backgroundColor: "transparent",
            "z-index": 2001,
        }
    });
}

function HideLoading() {
    $.unblockUI();
}

function RequestGET(mostrarLoading, url, AfterFunction) {
    return $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        success: function (resultado) {
            AfterFunction(resultado);
        },
        error: function () {
            var resultErro = { Sucesso: false, Mensagem: 'Function currently unavailable' };
            AfterFunction(resultErro);
        },
        beforeSend: function () { if (mostrarLoading) ShowLoading() },
        complete: function () { if (mostrarLoading) HideLoading() }
    });
}

function RequestPOST(mostrarLoading, url, data, AfterFunction) {
    return $.ajax({
        url: url,
        type: 'post',
        dataType: 'json',
        data: data,
        contentType: DeterminarContentType(data),
        success: function (resultado) {
            AfterFunction(resultado);
        },
        error: function () {
            var resultErro = { Sucesso: false, Mensagem: 'Function currently unavailable' };
            AfterFunction(resultErro);
        },
        beforeSend: function () { if (mostrarLoading) ShowLoading() },
        complete: function () { if (mostrarLoading) HideLoading() }
    });
}

function DeterminarContentType(data) {
    if (typeof data === 'string') {
        // If it's a query string
        if (data.includes('=')) {
            return 'application/x-www-form-urlencoded';
        }
        // If it's JSON
        try {
            JSON.parse(data);
            return 'application/json';
        } catch (error) {
            // Not valid JSON
        }
    } else if (Array.isArray(data)) {
        // If it's form data serialized using jQuery.serializeArray()
        return 'application/x-www-form-urlencoded';
    }
    // Default to plain text
    return 'text/plain';
}

function LimitarCaracteres(texto, tamanho) {
    if (texto != null) {
        if (tamanho != null && !isNaN(tamanho) && tamanho > 3 && texto.length > tamanho) {
            return texto.substr(0, tamanho - 3) + '..'
        }
        else
            return texto
    }
    else
        return ''
}

function FormatarData(data) {
    if (data != null && data != "") {

        var date = new Date(data);
        var month = date.getMonth() + 1;
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var retorno = '';
        retorno = ("0" + date.getDate()).slice(-2) + "/" + ("0" + month).slice(-2) + "/" + date.getFullYear();
        if ((hours == "" || hours == "00" || hours == "0") && (minutes == "" || minutes == "00" || minutes == "0")) {
            return retorno;
        }
        else {
            return retorno += " " + ("0" + hours).slice(-2) + ":" + ("0" + minutes).slice(-2);
        }
    }
    else {
        return '';
    }
}

function getQueryStringParamByName(name) {
    var regexS = "[\\?&]" + name + "=([^&#]*)",
        regex = new RegExp(regexS),
        results = regex.exec(window.location.search);
    if (results === null) {
        return "";
    } else {
        return decodeURIComponent(results[1].replace(/\+/g, " "));
    }
}

function ToggleFiltro() {
    $('.div-filtro').slideToggle();
}

function InitializeCharacterCounter(idInput, idSpan) {
    $("#" + idSpan).html(($(`#${idInput}`).val()?.length || 0) + "/" + $(`#${idInput}`).attr('maxlength'));

    $(document).on("input keyup", "#" + idInput, function () {
        $("#" + idSpan).text($(this).val().length + "/" + $(this).attr('maxlength'));
    });
}

function Redirecionar(url) {
    window.location.href = url;
}

function ValidarCPFCNPJ(valor) {
    if (valor.replace(/\D/g, '').length == 11)
        return ValidarCPF(valor);
    else
        return ValidarCNPJ(valor);
}

function ValidarCNPJ(cnpj) {
    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length < 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" || cnpj == "11111111111111" || cnpj == "22222222222222" || cnpj == "33333333333333" || cnpj == "44444444444444" ||
        cnpj == "55555555555555" || cnpj == "66666666666666" || cnpj == "77777777777777" || cnpj == "88888888888888" || cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false

    return true;
}

function ValidarCPF(cpf) {
    exp = /\.|\-/g
    cpf = cpf.toString().replace(exp, "");

    if (cpf.length != 11 || cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" ||
        cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" ||
        cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
        return false;

    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;
    if (digitoGerado != digitoDigitado)
        return false;

    return true;
}

function InstanciarLerMais() {
    var showChar = 30;
    var ellipsestext = "";
    var moretext = "...";
    var lesstext = " <-";

    $('.more').each(function () {
        if (!isNaN(parseInt(this.dataset.showchar)))
            showChar = parseInt(this.dataset.showchar);

        var content = $(this).html();

        if (content.length > showChar && $(this).find('.morecontent').length == 0) {
            var c = content.substr(0, showChar);
            var h = content.substr(showChar, content.length - showChar);

            var html = c + '<span class="moreellipses">' + ellipsestext + '&nbsp;</span><span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';

            $(this).html(html);
        }
    });

    $(".morelink").unbind('click').click(function () {
        if ($(this).hasClass("less")) {
            $(this).removeClass("less");
            $(this).html(moretext);
        } else {
            $(this).addClass("less");
            $(this).html(lesstext);
        }
        $(this).parent().prev().toggle();
        $(this).prev().toggle();
        return false;
    });
}

function AfterRequisicaoReload(resultado) {
    if (!resultado.Sucesso)
        swal('', HtmlEncode(resultado.Mensagem));
    else {
        toastr.success('Ok!');
        window.location.reload();
    }
}

function GerarBlobParaDownload(bytes, nomeArquivo) {
    //Convert Byte Array to BLOB.
    var blob = new Blob([bytes], { type: "application/octetstream" });
    //Check the Browser type and download the File.
    var isIE = false || !!document.documentMode;
    if (isIE) {
        window.navigator.msSaveBlob(blob, resultado.Retorno.Nome);
    } else {
        var url = window.URL || window.webkitURL;
        link = url.createObjectURL(blob);
        var a = $("<a />");
        a.attr("download", nomeArquivo);
        a.attr("href", link);
        a[0].click();
    }
}

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

function AfterBaixarAnexo(resultado) {
    if (resultado.Sucesso) {
        GerarBlobParaDownload(Base64ToBytes(resultado.Retorno.Anexo), resultado.Retorno.Nome);
    } else {
        swal('', HtmlEncode(resultado.Mensagem));
    }
}

function HtmlEncode(rawStr) {
    if (rawStr == undefined || rawStr == null || rawStr == '')
        return '';
    else {
        var encodedStr = rawStr.replace(/[\u00A0-\u9999<>\&]/g, function (i) {
            return '&#' + i.charCodeAt(0) + ';';
        });
        return encodedStr;
    }
}

function RemoverAcentosCaracteresEspeciais(palavra) {
    // Converte a palavra para uma string comum e remove espaços extras
    var str = palavra.toString().trim();

    // Define os caracteres a serem substituídos e seus equivalentes
    const mapaAcentos = {
        'a': /[àáâãäå]/g,
        'e': /[èéêë]/g,
        'i': /[ìíîï]/g,
        'o': /[òóôõö]/g,
        'u': /[ùúûü]/g,
        'c': /[ç]/g,
        'n': /[ñ]/g
    };

    // Faz a substituição dos caracteres acentuados e especiais pelos seus equivalentes
    for (let letra in mapaAcentos) {
        const regex = mapaAcentos[letra];
        str = str.replace(regex, letra);
    }

    // Remove todos os caracteres especiais que não sejam letras ou números
    str = str.replace(/[^a-z0-9\s]/g, '');

    // Retorna a palavra sem acentos ou caracteres especiais
    return str;
}

function NewId() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
        .replace(/[xy]/g, function (c) {
            const r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
}

function InstanciarMultiSelect() {
    $(".multi-filtro").css("width", "100%");
    $(".multi-filtro").select2({
        allowClear: true,
        theme: 'bootstrap'
    });

    $('.select2-selection--multiple').css('overflow', 'auto')
}

var coresPadrao = ["#6A5ACD", "#836FFF", "#6959CD", "#483D8B", "#191970", "#000080", "#00008B", "#0000CD", "#0000FF", "#6495ED", "#4169E1", "#1E90FF", "#00BFFF", "#87CEFA", "#87CEEB", "#ADD8E6", "#4682B4", "#B0C4DE", "#708090", "#778899", "#00FFFF", "#00CED1", "#40E0D0", "#48D1CC", "#20B2AA", "#008B8B", "#008080", "#7FFFD4", "#66CDAA", "#5F9EA0", "#2F4F4F", "#00FA9A", "#00FF7F", "#98FB98", "#90EE90", "#8FBC8F", "#3CB371", "#2E8B57", "#006400", "#008000", "#228B22", "#32CD32", "#00FF00", "#7CFC00", "#7FFF00", "#ADFF2F", "#9ACD32", "6B8E23", "#556B2F", "#808000", "#BDB76B", "#DAA520", "#B8860B", "#8B4513", "#A0522D", "#BC8F8F", "#CD853F", "#D2691E", "#F4A460", "#FFDEAD", "#F5DEB3", "#DEB887", "#D2B48C", "#7B68EE", "#9370DB", "#8A2BE2", "#4B0082", "#9400D3", "#9932CC", "#BA55D3", "#A020F0", "#8B008B", "#FF00FF", "#EE82EE", "#DA70D6", "#DDA0DD"];