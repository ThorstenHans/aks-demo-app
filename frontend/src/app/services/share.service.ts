import { Session } from "../models/session";
import { Observable } from "rxjs";

export abstract class ShareService {
  public abstract shareSession(target: string, session: Session): Observable<any>;
}
