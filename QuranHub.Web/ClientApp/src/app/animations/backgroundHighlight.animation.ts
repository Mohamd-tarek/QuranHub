import { trigger, style, state, transition, animate } from "@angular/animations";

export const BackgroundHighlightTrigger = trigger("backgroundHighlight",[ 
  transition("void => *", animate("5s 5ms ease-out", style({backgroundColor: "white"})))
  ])