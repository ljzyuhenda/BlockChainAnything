using System;
using NBitcoin;

namespace blockchain {
    public class Chapter1 {
        Script scriptPublicKeyFromHash;
        Script scriptPublicKeyFromAddress;
        Key keyNew;

        public void lesson1 () {
            keyNew = new Key ();
            PubKey pubKey = keyNew.PubKey;
            Console.WriteLine ("Public Key:{0}", pubKey);

            KeyId hashId = pubKey.Hash;
            Console.WriteLine ("Hashed Public Key{0}", hashId);

            BitcoinAddress address = pubKey.GetAddress (Network.Main);
            Console.WriteLine ("Address:{0}", address);

            scriptPublicKeyFromAddress = address.ScriptPubKey;
            Console.WriteLine ("ScriptPubKey from address:{0}", scriptPublicKeyFromAddress);

            scriptPublicKeyFromHash = hashId.ScriptPubKey;
            Console.WriteLine ("ScriptPubKey from hash:{0}", scriptPublicKeyFromHash);
        }

        public void lesson2 () {
            Script scriptPubkey = new Script (scriptPublicKeyFromAddress.ToString ());
            BitcoinAddress address = scriptPubkey.GetDestinationAddress (Network.Main);
            Console.WriteLine ("Bitcoin Address:{0}", address);
        }

        public void lesson3 () {
            Script scriptPubkey = new Script (scriptPublicKeyFromAddress.ToString ());
            KeyId hash = (KeyId) scriptPubkey.GetDestination ();
            BitcoinPubKeyAddress bitcoinAddress = new BitcoinPubKeyAddress (hash, Network.Main);
            Console.WriteLine ("Bitcoin Address:{0}", bitcoinAddress);
        }

        public static Chapter1 operator + (Chapter1 chaptera, Chapter1 chapterb) {
            Chapter1 chapter = new Chapter1 ();

            return chapter;
        }

        public void lesson4 () {
            if (keyNew == null) {
                keyNew = new Key ();
            }

            BitcoinSecret secret = keyNew.GetBitcoinSecret (Network.Main);
            Console.WriteLine ("Bitcoin secret:{0}", secret);

            // Transaction payment = new Transaction();
            // payment.Inputs.Add(new TxIn{
            //     PrevOut = new OutPoint()
            // });

            BitcoinEncryptedSecret encryptedSecret = secret.Encrypt ("secret");
            Console.WriteLine (encryptedSecret);
            secret = encryptedSecret.GetSecret ("secret");
            Console.WriteLine (secret);

            BitcoinPassphraseCode passphraseCode = new BitcoinPassphraseCode("my secret", Network.Main, null);
            EncryptedKeyResult keyResult1 =  passphraseCode.GenerateEncryptedSecret();
            Console.WriteLine("bitcoin address:{0}", keyResult1.GeneratedAddress);
            Console.WriteLine("bitcoin encrpted key{0}", keyResult1.EncryptedKey);
            Console.WriteLine("bitcoin confirmcode:{0}", keyResult1.ConfirmationCode);

            /**
                输入码 -> phraseCode ->生成加密秘钥、地址、认证信息

                加密秘钥 + 输入码 -> 私钥 ->地址
                认证信息 + 输入码 -> 地址
             */

             ExtKey masterKey = new ExtKey();
             Console.WriteLine("Master key:" + masterKey.ToString(Network.Main));
             for(int index = 0; index < 5; index++){
                ExtKey key = masterKey.Derive((uint)index);
                Console.WriteLine("Key" + index + ":" + key.ToString(Network.Main));
             }
        }
    }
}