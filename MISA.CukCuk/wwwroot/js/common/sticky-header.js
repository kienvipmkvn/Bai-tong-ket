/**
 * giữ header cố định
 * CreatedBy: dtkien1 (10/12/2020)
 * */
$(document).ready(function () {
    var $th = $('.table').find('thead th')
    $('.grid').scroll(function (){
        $th.css('transform', 'translateY(' + this.scrollTop + 'px)')
    })
})