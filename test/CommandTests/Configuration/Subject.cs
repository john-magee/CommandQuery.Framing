using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Fixie;
using System;

namespace CommandTests.Configuration
{
    public abstract class Subject<TClassUnderTest>
        : ISubjectBase
        where TClassUnderTest : class
    {
        protected IFixture _fixture;

        private TClassUnderTest _sut;

        protected TClassUnderTest Sut
        {
            get { return _sut ??= new Lazy<TClassUnderTest>(() => _fixture.Create<TClassUnderTest>()).Value; }
        }

        protected Subject()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
                GenerateDelegates = true
            });
        }

        public virtual void FixtureSetup()
        {
        }

        public virtual void FixtureTearDown()
        {
            _fixture.Dispose();
        }

        protected void Register<TInterface>(TInterface concreteType)
        {
            _fixture.Register(() => concreteType);
        }

        protected T MockType<T>()
        {
            return _fixture.Create<T>();
        }
    }
}