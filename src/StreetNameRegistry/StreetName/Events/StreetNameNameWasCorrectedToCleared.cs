namespace StreetNameRegistry.StreetName.Events
{
    using System;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;

    [EventName("StreetNameNameWasCorrectedToCleared")]
    [EventDescription("De straatnaam naam werd gewist via correctie.")]
    public class StreetNameNameWasCorrectedToCleared : IHasStreetNameId, IHasProvenance, ISetProvenance
    {
        [EventPropertyDescription("Interne GUID van de straatnaam.")]
        public Guid StreetNameId { get; }
        
        [EventPropertyDescription("Taal (voluit, EN) waarvoor de officiële naam gewist werd.")]
        public Language? Language { get; }
        
        [EventPropertyDescription("Metadata bij het event.")]
        public ProvenanceData Provenance { get; private set; }

        public StreetNameNameWasCorrectedToCleared(
            StreetNameId streetNameId,
            Language? language)
        {
            StreetNameId = streetNameId;
            Language = language;
        }

        [JsonConstructor]
        private StreetNameNameWasCorrectedToCleared(
            Guid streetNameId,
            Language? language,
            ProvenanceData provenance) :
            this(
                new StreetNameId(streetNameId),
                language) => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
