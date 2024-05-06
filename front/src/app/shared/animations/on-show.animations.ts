import { animate, state, style, transition, trigger } from "@angular/animations";

export const STARTER_ANIMATIONS = [
  trigger('stratFalling', [
    state('void', style({
      opacity: -0,
      top: -4
    })),
    transition('void => *', animate(200)),
  ]),
  trigger('startUpward', [
    state('void', style({
      opacity: -0,
      top: 10
    })),
    transition('void => *', animate('300ms ease-out')),
  ]),
  trigger('stratToRight', [
    state('void', style({
      opacity: 0,
      right: -100,
    })),
    transition('* => *', animate('300ms ease-out'))
  ])
];
