public sealed class PlayableArea : Singleton<PlayableArea>
{
	public static readonly int pl1AreaLeftLine = -80;
	public static readonly int pl1AreaRightLine = -8;
	public static readonly int pl2AreaLeftLine = -1 * pl1AreaRightLine;
	public static readonly int pl2AreaRightLine = -1 * pl1AreaLeftLine;
	public static readonly int playAreaTopLine = 44;
	public static readonly int playAreaBottomLine = -55;
}