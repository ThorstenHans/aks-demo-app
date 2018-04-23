import { Component, OnInit } from '@angular/core';
import { Session } from '../../../models/session';
import { SessionsService } from '../../../services/sessions.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'sessions-sessionslist',
  templateUrl: 'sessions-list.component.html',
})
export class SessionsListComponent implements OnInit {
  public sessions: Array<Session> = [];
  constructor(
    private readonly _router: Router,
    private readonly _route: ActivatedRoute,
    private readonly _sessionsService: SessionsService
  ) {}

  public ngOnInit() {
    this._sessionsService.getAllSessions().subscribe(sessions => (this.sessions = sessions));
  }

  public navigateToCreate() {
    this._router.navigate(['../create'], { relativeTo: this._route });
  }
}
