import { RootComponent } from '../components/root/root.component';
import { SessionsListComponent } from '../components/sessions/list/sessions-list.component';
import { SessionDetailsComponent } from '../components/sessions/details/session-details.component';
import { SessionResolver } from '../resolvers/session.resolver';

export const ALL_ROUTES = [
  { path: '', pathMatch: 'full', redirectTo: 'sessions/list' },
  {
    path: 'sessions',
    children: [
      { path: 'list', component: SessionsListComponent },
      { path: 'create', component: SessionDetailsComponent },
      { path: 'details/:id', component: SessionDetailsComponent, resolve: { session: SessionResolver } },
    ],
  },
];
