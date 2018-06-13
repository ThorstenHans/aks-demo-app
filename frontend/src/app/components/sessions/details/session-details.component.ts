import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Session } from '../../../models/session';
import { SessionsService } from '../../../services/sessions.service';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import { SessionResolver } from '../../../resolvers/session.resolver';
import { ExportService } from '../../../services/export.service';

@Component({
  selector: 'sessions-sessiondetails',
  templateUrl: 'session-details.component.html',
})
export class SessionDetailsComponent implements OnInit {
  public session: FormGroup;
  public sessionObject: Session;
  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _sessionsService: SessionsService,
    private readonly _exportService: ExportService,
    private readonly _router: Router,
    private readonly _route: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this._route.data.subscribe((data: { session: Session }) => {
      this.sessionObject = data.session || new Session();
    });
    const isDisabled = !this.sessionObject.id;
    this.session = this._formBuilder.group({
      id: new FormControl({ value: this.sessionObject.id, disabled: true }),
      upVotes: new FormControl({ value: this.sessionObject.upVotes, disabled: true }),
      downVotes: new FormControl({ value: this.sessionObject.downVotes, disabled: true }),
      mail: new FormControl({ value: null, disabled: false }),
      title: new FormControl(
        {
          value: this.sessionObject.title,
          disabled: isDisabled,
        },
        Validators.compose([Validators.required, Validators.maxLength(255)])
      ),
      speaker: new FormControl(
        {
          value: this.sessionObject.speaker,
          disabled: isDisabled,
        },
        Validators.compose([Validators.required, Validators.maxLength(255)])
      ),
      abstract: new FormControl(
        {
          value: this.sessionObject.abstract,
          disabled: isDisabled,
        },
        Validators.required
      ),
      conference: new FormControl(
        {
          value: this.sessionObject.conference,
          disabled: isDisabled,
        },
        Validators.required
      ),
      level: new FormControl({
        value: this.sessionObject.level,
        disabled: isDisabled,
      }),
      audience: new FormControl({
        value: this.sessionObject.audience,
        disabled: isDisabled,
      }),
    });
  }

  public get title(): AbstractControl {
    return this.session.get('title');
  }

  public get conference(): AbstractControl {
    return this.session.get('conference');
  }

  public get speaker(): AbstractControl {
    return this.session.get('speaker');
  }

  public get abstract(): AbstractControl {
    return this.session.get('abstract');
  }

  public onSubmit(): void {
    const sessionModel = Session.fromFormModel(this.session.value);

    this._sessionsService.createSession(sessionModel).subscribe(() => this._navigateToList());
  }

  public getAsPdf() {
    this._exportService.generatePdf(this.sessionObject.id, this.session.value.mail).subscribe();
  }

  public deleteSession(): void {
    this._sessionsService.deleteSession(this.sessionObject.id).subscribe(() => this._navigateToList());
  }

  private _navigateToList(): void {
    this._router.navigate(['../list'], { relativeTo: this._route });
  }
}
