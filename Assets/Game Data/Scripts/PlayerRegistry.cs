using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerRegistry {

    // keeps a list of all the players
    static List<Player> players = new List<Player>();

    public static List<IPlayer> connectedPlayers = new List<IPlayer>();
    public static List<BoltEntity> connectedEntities = new List<BoltEntity>();

    // create a player for a connection
    // note: connection can be null
    static Player CreatePlayer(BoltConnection connection)
    {
        Player p;

        // create a new player object, assign the connection property
        // of the object to the connection was passed in
        p = new Player();
        p.connection = connection;

        // if we have a connection, assign this player
        // as the user data for the connection so that we
        // always have an easy way to get the player object
        // for a connection
        if (p.connection != null)
        {
            p.connection.UserData = p;
        }

        // add to list of all players
        players.Add(p);

        return p;
    }

    // this simply returns the 'players' list cast to
    // an IEnumerable<T> so that we hide the ability
    // to modify the player list from the outside.
    public static IEnumerable<Player> allPlayers
    {
        get { return players; }
    }

    // finds the server player by checking the
    // .isServer property for every player object.
    public static Player serverPlayer
    {
        get { return players.First(x => x.isServer); }
    }

    // utility function which creates a server player
    public static Player CreateServerPlayer()
    {
        return CreatePlayer(null);
    }

    // utility that creates a client player object.
    public static Player CreateClientPlayer(BoltConnection connection)
    {
        return CreatePlayer(connection);
    }

    // utility function which lets us pass in a
    // BoltConnection object (even a null) and have
    // it return the proper player object for it.
    public static Player GetPlayer(BoltConnection connection)
    {
        if (connection == null)
        {
            return serverPlayer;
        }

        return (Player)connection.UserData;
    }
}
