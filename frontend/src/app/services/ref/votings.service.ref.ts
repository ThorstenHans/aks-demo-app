import { VotingsService } from '../votings.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { VotingSummary } from '../../models/votingSummary';
import { mapTo } from 'rxjs/operators';
import { Voting } from '../../models/voting';

@Injectable()
export class VotingsServiceRef extends VotingsService {
  constructor(private readonly _http: HttpClient) {
    super();
  }

  public voteUp(sessionId: string): Observable<boolean> {
    return this._vote(sessionId, 1);
  }
  public voteDown(sessionId: string): Observable<boolean> {
    return this._vote(sessionId, -1);
  }

  public getVotingSummary(sessionId: string): Observable<VotingSummary> {
    return this._http.get<VotingSummary>(`http://localhost:8082/api/votings/${sessionId}`);
  }

  private _vote(sessionId: string, change: number): Observable<boolean> {
    return this._http.post<boolean>(`http://localhost:8082/api/votings/${sessionId}`, {
      change: change,
    });
  }
}
