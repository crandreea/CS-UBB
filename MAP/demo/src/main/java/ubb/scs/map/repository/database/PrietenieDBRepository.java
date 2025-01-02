package ubb.scs.map.repository.database;

import ubb.scs.map.database.DBConnection;
import ubb.scs.map.domain.Prietenie;
import ubb.scs.map.domain.Tuplu;
import ubb.scs.map.domain.validators.PrietenieValidator;


import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class PrietenieDBRepository extends AbstractDBRepository<Tuplu<Long, Long>, Prietenie> {

    private Connection connection;

    public PrietenieDBRepository(PrietenieValidator validator) {
        super(validator);
        this.connection = DBConnection.getInstance().getConnection();
    }

    @Override
    public Iterable<Prietenie> findAll() {
        List<Prietenie> entities = new ArrayList<>();

        try (PreparedStatement statement = findAllQuery()) {
            try (ResultSet resultSet = statement.executeQuery()) {
                while (resultSet.next()) {
                    Long id1 = resultSet.getLong("uid1");
                    Long id2 = resultSet.getLong("uid2");
                    Prietenie entity = buildEntity(resultSet);
                    entity.setId(new Tuplu<>(id1, id2));
                    entities.add(entity);
                }
            }
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        return entities;
    }

    @Override
    public Optional<Prietenie> save(Prietenie entity) {
        if (entity == null) {
            throw new IllegalArgumentException("Entity-ul nu poate fi null!");
        }

        try (PreparedStatement statement = saveQuery(entity)) {
            statement.executeUpdate();
            try (ResultSet resultSet = statement.getGeneratedKeys()) {
                if (resultSet.next()) {
                    Long id1 = resultSet.getLong("uid1");
                    Long id2 = resultSet.getLong("uid2");
                    entity.setId(new Tuplu<>(id1, id2));
                }
            }
            return Optional.of(entity);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    protected PreparedStatement findOneQuery(Tuplu<Long, Long> id_pr) throws SQLException {
        String query = "SELECT * FROM prietenii WHERE uid1 = ? AND uid2 = ?";
        PreparedStatement statement = connection.prepareStatement(query);
        statement.setObject(1, id_pr.getE1());
        statement.setObject(2, id_pr.getE2());
        return statement;
    }

    @Override
    protected PreparedStatement findAllQuery() throws SQLException {
        String query = "SELECT * FROM prietenii";
        PreparedStatement statement = connection.prepareStatement(query);
        return statement;
    }

    @Override
    protected PreparedStatement saveQuery(Prietenie entity) throws SQLException {
        String query = "INSERT INTO prietenii(uid1, uid2, date) VALUES (?, ?, ?)";
        PreparedStatement statement = connection.prepareStatement(query);
        statement.setObject(1, entity.getIdUser1());
        statement.setObject(2, entity.getIdUser2());
        statement.setObject(3, entity.getDate());
        return statement;
    }

    @Override
    protected PreparedStatement deleteQuery(Tuplu<Long, Long> id_pr) throws SQLException {
        String query = "DELETE FROM prietenii WHERE uid1 = ? AND uid2 = ?";
        PreparedStatement statement = connection.prepareStatement(query);
        statement.setObject(1, id_pr.getE1());
        statement.setObject(2, id_pr.getE2());
        return statement;
    }

    @Override
    protected PreparedStatement updateQuery(Prietenie entity) throws SQLException {
        return null;
    }

    @Override
    protected Prietenie buildEntity(ResultSet resultSet) throws SQLException {
        Long uid1 = resultSet.getLong("uid1");
        Long uid2 = resultSet.getLong("uid2");
        Prietenie pr = new Prietenie(uid1, uid2);
        return pr;
    }


}
