export default function SectionButton({
  className,
  children,
  onSelect,
  isDisabled,
}) {
  return (
    <button className={`${className}`} onClick={onSelect} disabled={isDisabled}>
      {children}
    </button>
  );
}
