export class Session {
  public id: string;
  public title: string;
  public abstract: string;
  public speaker: string;
  public conference: string;
  public audience: number;
  public level: number;
  public downVotes: number;
  public upVotes: number;

  public static fromFormModel(formModel: any): Session {
    const session = new Session();
    session.id = formModel['id'];
    session.level = +formModel['level'];
    session.title = formModel['title'];
    session.speaker = formModel['speaker'];
    session.conference = formModel['conference'];
    session.abstract = formModel['abstract'];
    session.audience = +formModel['audience'];
    session.upVotes = +formModel['upVotes'];
    session.downVotes = +formModel['downVotes'];
    return session;
  }
}
