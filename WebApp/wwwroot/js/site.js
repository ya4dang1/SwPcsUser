// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function getCookie(cname) {
  var name = cname + "=";
  var decodedCookie = decodeURIComponent(document.cookie);
  var ca = decodedCookie.split(';');
  for(var i = 0; i <ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0) == ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}

function getIcon(text, texttype = 'light') {
  var html = '';
  switch (text) {
    case true:
      html = '<i class="fa fa-check text-success"></i>';
      break;
    case false:
      html = '<i class="fas fa-times text-danger"></i>';
      break;
    case 'New':
      html = '<h5><span class="badge badge-warning">New</span></h5>';
      break;
    case 'Activated':
    case 'Registered':
      html = '<h5><span class="badge badge-success">Registered</span></h5>';
      break;
    case 'Deactivated':
    case 'Unregistered':
      html = '<h5><span class="badge badge-danger">Unregistered</span></h5>';
      break;
    case 'Pending':
      html = '<h5><span class="badge badge-primary">Pending</span></h5>';
      break;
    case 'PendingApproval':
      html = '<h5><span class="badge badge-primary text-nowrap">Pending Approval</span></h5>';
      break;
    case 'PendingAction':
      html = '<h5><span class="badge badge-primary text-nowrap">Pending Action</span></h5>';
      break;
    case 'Processing':
      html = '<h5><span class="badge badge-primary">Processing</span></h5>';
      break;
    case 'Submitted':
      html = '<h5><span class="badge badge-primary">Submitted</span></h5>';
      break;
    case 'CompletedWithError':
      html = '<h5><span class="badge badge-warning text-nowrap">Completed With Error</span></h5>';
      break;
    case 'Completed':
      html = '<h5><span class="badge badge-success">Completed</span></h5>';
      break;
    case 'Failed':
      html = '<h5><span class="badge badge-danger">Failed</span></h5>';
      break;
    default:
      html = '<h5><span class="badge badge-' + texttype + '">' + text + '</span></h5>';
      break;
  }
  return html;
}

function GetURLParameter(sParam) {
  var sPageURL = window.location.search.substring(1);
  var sURLVariables = sPageURL.split('&');
  for (var i = 0; i < sURLVariables.length; i++) {
    var sParameterName = sURLVariables[i].split('=');
    if (sParameterName[0] == sParam) {
      return sParameterName[1];
    }
  }
}

function tableResize() {
  $('table').css('visibility','hidden');

  $('[class*=table-responsive]').addClass('dataTables_resize');
  if($('[class*=table-responsive]').width() > $('[class*=table-responsive]').parent().width()){
    $('[class*=table-responsive]').removeClass('dataTables_resize');    
  }
  
  clearTimeout(window.resizedFinished);
  window.resizedFinished = setTimeout(function(){
    $('[class*=table-responsive]').each(function (){
      isContent = ($(this).width() - $('thead', this).width());
      if(isContent > 5) {
        $(this).addClass('dataTables_resize');
      } else {
        // Do something          
      }
    });
  }, 250);
  $('table').css('visibility','visible');
}

let Localizer = class{
  constructor() {
    this.defaultCulture = "c=en-US|uic=en-US";
    this.culture = getCookie(".AspNetCore.Culture");
    this.translations = new Array();
    this.translations = {
      defaultCulture: { },
      "c=ja-JP|uic=ja-JP": {
        "No data available": "データなし",
        "Showing _START_ to _END_ of _TOTAL_ entries": "_TOTAL_件のうち_START_〜_END_を表示しています",
        "No entries found": "エントリが見つかりません",
        "(filtered from _MAX_ total entries)": "(_MAX_件からフィルタリング)",
        "Show _MENU_ entries": "_MENU_件を表示",
        "Loading...": "ロード中...",
        "Processing...": "処理中...",
        "Search:": "検索する:",
        "No matching records found": "一致するレコードが見つかりません",
        "First": "最初",
        "Last": "最後",
        ": activate to sort column ascending": ": 列を昇順に並べ替えます",
        ": activate to sort column descending": ": 列を降順に並べ替えます"
      }
    };
  }

  getTranslation(defaultText) {
    var translatedText = defaultText;
    if(this.culture != this.defaultCulture){
      translatedText = this.translations[this.culture][defaultText] != undefined ? this.translations[this.culture][defaultText] : defaultText;
    }
    
    return translatedText;
  }
}