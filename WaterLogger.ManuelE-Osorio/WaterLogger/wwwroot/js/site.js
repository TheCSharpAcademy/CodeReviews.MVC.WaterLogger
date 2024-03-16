// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*!
 * Color mode toggler for Bootstrap's docs (https://getbootstrap.com/)
 * Copyright 2011-2024 The Bootstrap Authors
 * Licensed under the Creative Commons Attribution 3.0 Unported License.
 */

document.getElementById('btnSwitch').addEventListener('click',()=>{
    if (document.documentElement.getAttribute('data-bs-theme') == 'dark') {
        document.documentElement.setAttribute('data-bs-theme','light') 
        document.querySelector("use[id=toggle-icon]").setAttribute('href','#sun-fill')
    }
    else {
        document.documentElement.setAttribute('data-bs-theme','dark')
        document.querySelector("use[id=toggle-icon]").setAttribute('href','#moon-stars-fill')
    }
})
  