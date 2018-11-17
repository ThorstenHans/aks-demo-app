import { VotingSummary } from '../models/votingSummary';
import { Observable } from 'rxjs';

export abstract class VotingsService {
  public abstract getVotingSummary(sessionId: string): Observable<VotingSummary>;
  public abstract voteUp(sessionId: string): Observable<boolean>;
  public abstract voteDown(sessionId: string): Observable<boolean>;
}
