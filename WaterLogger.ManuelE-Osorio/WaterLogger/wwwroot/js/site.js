// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function() {
    if(getCookie("isDark").includes("true")){
        themeSwitch()
    }
}

document.getElementById('btnSwitch').addEventListener('click',()=>{
    themeSwitch()
})

function themeSwitch()
{
    if (document.documentElement.getAttribute('data-bs-theme') == 'dark') {
        document.documentElement.setAttribute('data-bs-theme','light') 
        setCookie("isDark", "false", 2)
        document.querySelector("use[id=toggle-icon]").setAttribute('href','#sun-fill')
    }
    else {
        document.documentElement.setAttribute('data-bs-theme','dark')
        setCookie("isDark", "true", 2)
        document.querySelector("use[id=toggle-icon]").setAttribute('href','#moon-stars-fill')
    }
}

function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
  }

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i <ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
  }