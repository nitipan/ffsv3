import { trigger, state, animate, style, transition } from '@angular/animations';

export function routerTransition() {

  return fadeInOut();
}

function fadeInOut() {
  return trigger('routerTransition', [
    state('void', style({ position: 'fixed', width: '100%' })),
    state('*', style({ position: 'fixed', width: '100%' })),
    transition(':enter', [   // :enter is alias to 'void => *'
      style({ opacity: 0 }),
      animate(400, style({ opacity: 1 }))
    ]),
    transition(':leave', [   // :leave is alias to '* => void'
      animate(400, style({ opacity: 0 }))
    ])
  ]);
}

