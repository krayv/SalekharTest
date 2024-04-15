namespace Scorewarrior.Test.Descriptors
{
    public abstract class ModifiedDescriptor
    {
        protected Descriptor _descriptor;
        protected DescriptorModifierWrap _wrap;

        public Descriptor Descriptor => _descriptor;

        public ModifiedDescriptor(Descriptor descriptor, DescriptorModifierWrap wrap)
        {
            _wrap = wrap;
            _descriptor = descriptor;
        }

        public void Modify()
        {
            _wrap.Modify(this);
        }

        public abstract void ResetValue();
    } 
}
