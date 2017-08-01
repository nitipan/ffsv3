import { trigger, state, animate, style, transition } from '@angular/animations';

export function routerTransition() {
  return noAnimation();
}
export function noAnimation() {
  return trigger('routerTransition', [
    // state('void', style({ position: 'fixed' })),
    // state('*', style({})),
    // transition(':enter', [   // :enter is alias to 'void => *'
    //   style({ opacity: 0 }),
    //   animate(400, style({ opacity: 1 }))
    // ]),
    // transition(':leave', [   // :leave is alias to '* => void'
    //   animate(200, style({ opacity: 0, position: 'fixed' }))
    // ])
  ]);
}
export function fadeInOut() {
  return trigger('routerTransition', [
    state('void', style({ position: 'fixed' })),
    state('*', style({})),
    transition(':enter', [   // :enter is alias to 'void => *'
      style({ opacity: 0 }),
      animate(400, style({ opacity: 1 }))
    ]),
    transition(':leave', [   // :leave is alias to '* => void'
      animate(200, style({ opacity: 0, position: 'fixed' }))
    ])
  ]);
}

