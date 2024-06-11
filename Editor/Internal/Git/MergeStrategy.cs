namespace Glitch9.Internal.Git
{
    /// <summary>
    /// MergeStrategy 열거형은 다양한 Git 병합 전략을 정의합니다.
    /// </summary>
    public enum MergeStrategy
    {
        /// <summary>
        /// FastForward 병합은 브랜치 포인터를 병합 대상 브랜치로 이동시켜 히스토리를 단순하게 유지합니다.
        /// </summary>
        FastForward,

        /// <summary>
        /// NoFastForward 병합은 병합 커밋을 생성하여 병합 과정을 명확하게 합니다.
        /// </summary>
        NoFastForward,

        /// <summary>
        /// Squash 병합은 여러 커밋을 하나로 압축하여 단일 커밋으로 병합합니다.
        /// </summary>
        Squash,

        /// <summary>
        /// Ours 병합은 충돌이 발생했을 때 현재 브랜치의 변경사항을 유지합니다.
        /// </summary>
        Ours,

        /// <summary>
        /// Theirs 병합은 충돌이 발생했을 때 병합 대상 브랜치의 변경사항을 유지합니다.
        /// </summary>
        Theirs
    }
}