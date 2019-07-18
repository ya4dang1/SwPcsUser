// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(function () {
  if ( ! $.fn.DataTable.isDataTable('.datatable') ) {
    $.fn.DataTable.ext.pager.numbers_length = 5;
    $('.datatable').dataTable({
      "order": [
        [($('th[class*="datatable-default-sort-col-"]').index() > 0 ? $(
          'th[class*="datatable-default-sort-col-"]').index() : $('th:not(".datatable-actions")').index()), ($(
          'th[class*="datatable-default-sort-col-desc"]').index() > 0 ? "desc" : "asc")]
      ],
      "columnDefs": [{
          "orderable": false,
          "targets": 'datatable-actions'
        },
        {
          "type": 'date',
          "targets": 'datatable-datetime'
        }
      ],
      "language": {
        "emptyTable": "No data available",
        "paginate": {
          "previous": "<<",
          "next": ">>"
        }
      },
      "pageLength": 10,
      "processing": true,
      "initComplete": function( settings, json ) {
        tableResize();
      }
    });  
  }

  $('.datatable').attr('style', 'border-collapse: collapse !important');
});