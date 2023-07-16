import { trigger, style, state, transition, animate } from "@angular/animations";

export const FadeOutTrigger = trigger("fadeOut",[ 
  transition("* => void", animate("1s", style({transform: "translateY(-100%)"})))
  ])