const toggle_switch = document.querySelector(".side-nav-toggle");
const main_container = document.querySelector(".main-container");
const s_nav_menus = document.querySelectorAll(".s-nav-menu");
const s_nav_menuregion = document.querySelector('.s-nav-menu-region');
const tool_tip = document.querySelector(".tool-tip");

let is_nav_open = true;

//To toggle the button
toggle_switch?.addEventListener("click", () => {
  //Whenever the toggle switch is selected we will also try to add the checked class to it.
  is_nav_open = toggle_switch?.classList.toggle("nav-open"); //This will basically add/remove a class.

  //Along with this try to add a class to the main container as well to indicate that the side bar has to be toggled.
  //In frameworks like vue/react, we would use some state property but for vanilla js/css/html, it would be easy to handle with classes
  main_container?.classList.toggle("nav-open");
  s_nav_menuregion?.classList.toggle("scroll-bar-hidden");

});

s_nav_menus?.forEach((menu) =>

  menu.addEventListener("mouseover", (elem) => {
    //Use elem to find the position.
    //Left hand side cannot have possible null reference.
    if (tool_tip != null && !is_nav_open ){
        tool_tip.style.top = `${elem.clientY}px`;
        tool_tip.style.visibility = "visible";
        //On sidenav closed, we are making the span collapsed and thus we are not receiving it here.
        let title = menu.querySelector("span").getAttribute('tag');
        tool_tip.innerText = title;
    }
    //Use menu to fetch the span and get the name.

    //Get the first span of the element
    // let item = elem.querySelector('span');
  })
);

s_nav_menus?.forEach((menu) =>
  menu.addEventListener("mouseleave", (elem) => {
    if (tool_tip != null){
        tool_tip.style.visibility = "collapse";
        tool_tip.innerText = "ToolTip";
    }
  })
);

s_nav_menus?.forEach((menu) =>
  menu.addEventListener("click", (elem) => {
    //On click remove a class from all s_nav_menus and add one here.
    s_nav_menus.forEach(m=> m.classList.remove('selected'));
    menu.classList.add('selected');
  })
);