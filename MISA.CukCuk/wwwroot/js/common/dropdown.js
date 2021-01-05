$(document).ready(function () {
    document.addEventListener("click", closeAllSelect);
})

function dropdownFunction(number) {
    var x, i, j, l, ll, selElmnt, a, b, c;
    /*look for any elements with the class "custom-select":*/
    x = document.getElementsByClassName(`custom-select custom-select${number}`);
    l = x.length;
    for (i = 0; i < l; i++) {
        selElmnt = x[i].getElementsByTagName("select")[0];
        ll = selElmnt.length;
        /*for each element, create a new DIV that will act as the selected item:*/
        a = document.createElement("DIV");
        a.setAttribute("class", "select-selected");
        a.setAttribute("tabindex", 0);
        if (x[i].className.includes(' no-border')) {
            a.classList.add('no-border');
        }
        a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
        x[i].appendChild(a);
        /*for each element, create a new DIV that will contain the option list:*/
        b = document.createElement("DIV");
        b.setAttribute("class", "select-items select-hide");
        for (j = 0; j < ll; j++) {
            /*for each option in the original select element,
            create a new DIV that will act as an option item:*/
            c = document.createElement("DIV");
            c.innerHTML = selElmnt.options[j].innerHTML;
            //set value for each element
            c.setAttribute("value", selElmnt.options[j].value);
            c.setAttribute("tabindex", 0);
            if (j == 0) c.setAttribute("class", "same-as-selected");
            c.addEventListener("click", function (e) {
                /*when an item is clicked, update the original select box,
                and the selected item:*/
                e.preventDefault();
                e.stopPropagation();
                var y, i, k, s, h, sl, yl;
                s = this.parentNode.parentNode.getElementsByTagName("select")[0];
                sl = s.length;
                h = this.parentNode.previousSibling;
                for (i = 0; i < sl; i++) {
                    if (s.options[i].innerHTML == this.innerHTML) {
                        s.selectedIndex = i;
                        h.innerHTML = this.innerHTML;
                        y = this.parentNode.getElementsByClassName("same-as-selected");
                        yl = y.length;
                        for (k = 0; k < yl; k++) {
                            y[k].removeAttribute("class");
                        }
                        this.setAttribute("class", "same-as-selected");
                        break;
                    }
                }
                h.click();
            });

            $(c).focus(function (e) {
                this.style.backgroundColor = "#E9EBEE"
                $(this).bind('keyup', selectByEnter);
            })

            if (j < ll - 1) {
                $(c).blur(function (e) {
                    this.style.removeProperty("background-color");
                    $(this).unbind('keyup');
                })
            }
            else {
                $(c).blur(function (e) {
                    this.style.removeProperty("background-color");
                    $(this).unbind('keyup');
                    if (e.relatedTarget.nextSibling != this) {
                        closeAllSelect();
                    }
                })
            }
            b.appendChild(c);
        }
        x[i].appendChild(b);
        a.addEventListener("click", function (e) {
            /*when the select box is clicked, close any other select boxes,
            and open/close the current select box:*/
            e.preventDefault();
            e.stopPropagation();
            closeAllSelect(this);
            this.nextSibling.classList.toggle("select-hide");
            this.classList.toggle("select-arrow-active");
            this.classList.toggle("select-selected-focus");
        });
        $(a).focus(function (e) {
            $(this).bind('keyup', openDropdown);
        })

        $(a).blur(function (e) {
            $(this).unbind('keyup')
        })
    }
}

function openDropdown (e) {
    if (e.originalEvent.key == "Enter") {
        closeAllSelect(this);
        $(e.target).next().removeClass('select-hide');
    }
}

function selectByEnter(e) {
    if (e.originalEvent.key == "Enter") {
        var el = e.target.parentNode;
        $(el).children(".same-as-selected").removeClass('same-as-selected');

        $(el).prev().text($(this).text());
        $(this).addClass("same-as-selected");
        this.click();

        closeAllSelect(this);
    }
}

function closeAllSelect(elmnt) {
    /*a function that will close all select boxes in the document,
    except the current select box:*/
    var x, y, i, xl, yl, arrNo = [];
    x = document.getElementsByClassName("select-items");
    y = document.getElementsByClassName("select-selected");
    xl = x.length;
    yl = y.length;
    for (i = 0; i < yl; i++) {
        if (elmnt == y[i]) {
            arrNo.push(i)
        } else {
            y[i].classList.remove("select-arrow-active");
            y[i].classList.remove("select-selected-focus");
        }
    }
    for (i = 0; i < xl; i++) {
        if (arrNo.indexOf(i)) {
            x[i].classList.add("select-hide");
        }
    }
}