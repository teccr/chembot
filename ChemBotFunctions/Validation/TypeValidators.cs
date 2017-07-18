// Dot Net Namespaces
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ChemBotFunctions.Validation
{
    /// <summary>
    /// Stub implementation that validates input values. 
    /// </summary>
    public static class TypeValidators
    {
        /// <summary>
        /// USer friendly strings for each Chemical property.
        /// </summary>
        public static readonly ImmutableDictionary<string, string> FRIENDLY_USER_NAMES_CHEMICAL_PROPERTIES = (
                new Dictionary<string, string>()
                {
                    { "MolecularFormula","Molecular Formula" }, { "MolecularWeight","Molecular Weight" },
                    { "CanonicalSMILES","Canonical Smiles" }, { "IsomericSMILES","Isomeric Smiles" },
                    { "InChI","International Chemical Identifier" }, { "IUPACName","IUPAC Name" },
                    { "XLogP", "XLogP3-AA" }, { "ExactMass", "Exact Mass" }, { "MonoisotopicMass", "Monoisotopic Mass" },
                    { "TPSA", "Topological Polar Surface Area" }, { "HBondDonorCount", "Hydrogen Bond Donor Count" },
                    { "HBondAcceptorCount", "Hydrogen Bond Acceptor Count" }, { "RotatableBondCount", "Rotatable Bond Count" },
                    { "HeavyAtomCount", "Heavy Atom Count" }, { "IsotopeAtomCount", "Isotope Atom Count" },
                    { "AtomStereoCount", "Atom Stereo Count" }, { "DefinedAtomStereoCount", "Defined Atom Stereocenter Count" },
                    { "UndefinedAtomStereoCount", "Undefined Atom Stereocenter Count" }, { "BondStereoCount", "Bond Stereo Count" },
                    { "DefinedBondStereoCount", "Defined Bond Stereocenter Count" }, { "UndefinedBondStereoCount", "Undefined Bond Stereocenter Count" },
                    { "CovalentUnitCount", "Covalently-Bonded Unit Count" }, { "Volume3D", "Conformer analytic volume" },
                    { "XStericQuadrupole3D", "Steric quadrupole length" }, { "YStericQuadrupole3D", "Steric quadrupole width" }, { "ZStericQuadrupole3D", "Steric quadrupole height" },
                    { "FeatureCount3D", "Features per compound count"}
                }
            ).ToImmutableDictionary();

        /// <summary>
        /// Permutations of the Chemical properties.
        /// </summary>
        public static readonly ImmutableDictionary<string, string> PERMUTATIONS_CHEMICAL_PROPERTIES = (
                new Dictionary<string, string>() {
                    { "flash card", "FlashCard" }, { "compound card", "FlashCard" }, { "chemical card", "FlashCard" }, { "card", "FlashCard" },
                    { "synonyms", "synonyms" }, { "synonym", "synonyms" },
                    { "mf", "MolecularFormula" } , { "molecular formula", "MolecularFormula" }, { "mol formula", "MolecularFormula" }, { "formula", "MolecularFormula" },
                    { "mw", "MolecularWeight" }, { "mol weight", "MolecularWeight" }, { "weight", "MolecularWeight" }, { "molecular weight", "MolecularWeight" },
                    { "cs", "CanonicalSMILES" }, { "canonical smiles", "CanonicalSMILES" }, { "csmiles", "CanonicalSMILES" }, { "smiles", "CanonicalSMILES" }, 
                    { "iso smiles", "IsomericSMILES" }, { "isomeric smiles", "IsomericSMILES" }, { "ismiles", "IsomericSMILES" },
                    { "inchi", "InChI" }, { "inchikey", "InChIKey" }, { "ikey", "InChIKey" },
                    { "name","IUPACName" }, { "iupac name","IUPACName" },{ "iupac","IUPACName" },{ "iname","IUPACName" },
                    { "xlogp","XLogP" }, { "log p","XLogP" },
                    { "exact mass", "ExactMass" },  { "mass", "ExactMass" },
                    { "mono iso mass","MonoisotopicMass" }, { "mono isotopic mass","MonoisotopicMass" }, { "isotopic mass","MonoisotopicMass" }, { "iso mass","MonoisotopicMass" }, { "monoisotopic mass","MonoisotopicMass" },
                    { "tpsa", "TPSA" }, { "complexity", "Complexity" }, { "charge", "Charge" },
                    { "h bond donor count", "HBondDonorCount" }, { "h bond donor", "HBondDonorCount" }, { "bond donor", "HBondDonorCount" }, { "hydrogen bond donor", "HBondDonorCount" },
                    { "h bond acceptor count", "HBondAcceptorCount" }, { "h bond acceptor", "HBondDonorCount" }, { "bond acceptor", "HBondDonorCount" },
                    { "rotatable bond count", "RotatableBondCount" }, { "rotatable bond", "RotatableBondCount" },
                    { "heavy atom count", "HeavyAtomCount" }, { "heavy atoms", "HeavyAtomCount" },
                    { "isotope atom count", "IsotopeAtomCount" }, { "isotope atoms", "IsotopeAtomCount" },
                    { "atom stereo count", "AtomStereoCount" }, { "atom stereo", "AtomStereoCount" },
                    { "defined atom stereo count", "DefinedAtomStereoCount" }, { "defined atom stereo", "DefinedAtomStereoCount" }, { "defined atom", "DefinedAtomStereoCount" },
                    { "undefined atom stereo count", "UndefinedAtomStereoCount" }, { "undefined atom stereo", "UndefinedAtomStereoCount" }, { "undefined atom", "UndefinedAtomStereoCount" },
                    { "bond stereo count", "BondStereoCount" }, { "bond stereo", "BondStereoCount" },
                    { "defined bond stereo count", "DefinedBondStereoCount" }, { "defined bond stereo", "DefinedBondStereoCount" },
                    { "undefined bond stereo count", "UndefinedBondStereoCount" }, { "undefined bond stereo", "UndefinedBondStereoCount" },
                    { "covalent unit count", "CovalentUnitCount" }, { "covalent unit", "CovalentUnitCount" }, { "covalent units", "CovalentUnitCount" }, { "covalent bonded units", "CovalentUnitCount" },
                    { "volume 3d", "Volume3D" }, { "conformer analytic volume", "Volume3D" },
                    { "steric quadrupole length", "XStericQuadrupole3D" }, { "steric quadrupole width", "YStericQuadrupole3D" }, { "steric quadrupole height", "ZStericQuadrupole3D" },
                    { "features per compound count" , "FeatureCount3D" }, { "features per compound" , "FeatureCount3D" }
                }
            ).ToImmutableDictionary();

        /// <summary>
        /// Validation of chemical properties.
        /// </summary>
        /// <param name="chemicalProperty">Name of the chemical property to search.</param>
        /// <returns>True if valid.</returns>
        public static bool IsValidChemicalProperty(string chemicalProperty)
        {
            if (string.IsNullOrEmpty(chemicalProperty)) return false;
            return PERMUTATIONS_CHEMICAL_PROPERTIES.ContainsKey(chemicalProperty);
        }

        /// <summary>
        /// List of valid search criteria types.
        /// </summary>
        public static readonly ImmutableArray<string> VALID_SEARCH_CRITERIA = (
                new string[] { "cid", "name", "smiles", "inchikey", "sid", "aid" }
            ).ToImmutableArray();

        /// <summary>
        /// Properties with Mass Units supperted by PubChem
        /// </summary>
        public static readonly ImmutableArray<string> MASS_PUBCHEM_PROPERTIES = (
                new string[] {
                    "MolecularWeight", "ExactMass",  "MonoisotopicMass"
                }
            ).ToImmutableArray();

        /// <summary>
        /// Validates if the search criteria type is valid.
        /// </summary>
        /// <param name="searchCriteria">Search Criteria type to validate.</param>
        /// <returns>True if valid.</returns>
        public static bool IsValidSearchCriteria(string searchCriteria)
        {
            return VALID_SEARCH_CRITERIA.Contains(searchCriteria);
        }
    }
}
